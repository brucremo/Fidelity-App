using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FidelityHub.Application.Interfaces;
using FidelityHub.Infrastructure.Features.Mail;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MediatR;
using Microsoft.Extensions.Hosting;
using FidelityHub.Infrastructure.Database;
using FidelityHub.Infrastructure.Readers;
using FidelityHub.Persistence;
using FidelityHub.Infrastructure.JWT;
using FidelityHub.Database.Entities.UsrSchema;
using FidelityHub.Application.Helpers.Api;
using SendGrid;
using Microsoft.Extensions.Logging;
using Constants = FidelityHub.Infrastructure.JWT.Constants;
using FidelityHub.Application.SignalR.Hubs;
using FidelityHub.Communication.Email;

namespace Web.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, ILoggerFactory logger)
        {
            Configuration = configuration;
            Logger = logger;
        }

        public IConfiguration Configuration { get; }
        public ILoggerFactory Logger { get; }
        readonly string MyAllowSpecificOrigins = "CORSOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Logger.AddLog4Net();

            services.AddSignalR().AddMessagePackProtocol();

            services.AddCors(options => options.AddPolicy(this.MyAllowSpecificOrigins, builder =>
            {
                builder
                .AllowCredentials()
                .WithOrigins(new string[] { "http://localhost:4200", "192.168.1.112:4200", "192.168.1.16" })
                .AllowAnyHeader()
                .AllowAnyMethod();
            }));

            #region Contexts & Services
            services.AddEntityFrameworkNpgsql().AddDbContext<AppDbContext>(options => options.UseNpgsql(Configuration.GetConnectionString("PostgreSQLDB")));
            services.AddEntityFrameworkNpgsql().AddDbContext<UserDbContext>(options => options.UseNpgsql(Configuration.GetConnectionString("PostgreSQLDB")));
            services.AddEntityFrameworkNpgsql().AddDbContext<RefDbContext>(options => options.UseNpgsql(Configuration.GetConnectionString("PostgreSQLDB")));
            services.AddEntityFrameworkNpgsql().AddDbContext<DboDbContext>(options => options.UseNpgsql(Configuration.GetConnectionString("PostgreSQLDB")));

            services.AddTransient<IAppSchemaDataReader, AppSchemaDataReader>();
            services.AddTransient<IUserSchemaDataReader, UserSchemaDataReader>();
            services.AddTransient<IRefSchemaDataReader, RefSchemaDataReader>();
            services.AddTransient<IDboSchemaDataReader, DboSchemaDataReader>();

            services.AddTransient<ITokenFactory, TokenFactory>();
            services.AddTransient<IJwtTokenHandler, JwtTokenHandler>();
            services.AddTransient<IJwtTokenValidator, JwtTokenValidator>();
            services.AddTransient<IJwtFactory, JwtFactory>();
            #endregion

            #region MediatR
            var assembly = AppDomain.CurrentDomain.Load("FidelityHub.Application");
            services.AddMediatR(assembly);
            #endregion

            #region SendGrid / Email
            var sendGridSettings = Configuration.GetSection(nameof(SendGridClientOptions));

            //services.Configure<SendGridClientOptions>(sendGridSettings);

            services.AddTransient<ISendGridConnector, SendGridConnector>();

            var smtpSettings = Configuration.GetSection(nameof(SmtpOptions));
            services.Configure<SmtpOptions>(smtpSettings);
            #endregion

            services.AddControllersWithViews();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            #region JWT/Auth Configuration
            // Register the ConfigurationBuilder instance of AuthSettings
            var authSettings = Configuration.GetSection(nameof(AuthSettings));
            services.Configure<AuthSettings>(authSettings);

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authSettings[nameof(AuthSettings.SecretKey)]));

            // jwt wire up
            // Get options from app settings
            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));

            // Configure JwtIssuerOptions
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

                ValidateAudience = true,
                ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                RequireExpirationTime = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;

                configureOptions.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }
                        return Task.CompletedTask;
                    },
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        // If the request is for our hub...
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken))
                        {
                            // Read the token out of the query string
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            // api user claim policy
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AuthenticatedUser", policy => policy.RequireClaim(Constants.Strings.JwtClaimIdentifiers.Rol, FidelityHub.Infrastructure.JWT.Constants.Strings.JwtClaims.AuthenticatedUser));
                options.AddPolicy("VendorAdmin", policy => policy.RequireClaim(Constants.Strings.JwtClaimIdentifiers.Rol, FidelityHub.Infrastructure.JWT.Constants.Strings.JwtClaims.VendorAdminAccess));
                options.AddPolicy("Vendor", policy => policy.RequireClaim(Constants.Strings.JwtClaimIdentifiers.Rol, FidelityHub.Infrastructure.JWT.Constants.Strings.JwtClaims.VendorAccess));
                options.AddPolicy("Customer", policy => policy.RequireClaim(Constants.Strings.JwtClaimIdentifiers.Rol, FidelityHub.Infrastructure.JWT.Constants.Strings.JwtClaims.CustomerAccess));
                options.AddPolicy("Admin", policy => policy.RequireClaim(Constants.Strings.JwtClaimIdentifiers.Rol, FidelityHub.Infrastructure.JWT.Constants.Strings.JwtClaims.AdminAccess));
            });

            // add identity
            var identityBuilder = services.AddIdentityCore<AspNetUser>(o =>
            {
                // configure identity options
                o.Password.RequireDigit = true;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 10;
            });

            identityBuilder = new IdentityBuilder(identityBuilder.UserType, typeof(IdentityRole), identityBuilder.Services);
            identityBuilder.AddEntityFrameworkStores<DboDbContext>().AddDefaultTokenProviders();

            services.Configure<DataProtectionTokenProviderOptions>(opt => opt.TokenLifespan = TimeSpan.FromHours(2));
            #endregion

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LoyalApp API", Version = "v1" });
            });

            services.AddHttpsRedirection(options =>
            {
                options.RedirectStatusCode = StatusCodes.Status308PermanentRedirect;
                options.HttpsPort = 443;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseExceptionHandler(
                builder =>
                {
                    builder.Run(
                        async context =>
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                            context.Response.Headers.Add("Access-Control-Allow-Origin", "*");

                            var error = context.Features.Get<IExceptionHandlerFeature>();
                            if (error != null)
                            {
                                await context.Response.WriteAsync(error.Error.Message).ConfigureAwait(false);
                            }
                        });
                });

            if (env.IsDevelopment())
            {
                // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
                // specifying the Swagger JSON endpoint.
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "AspNetCoreApiStarter V1");
                    c.RoutePrefix = "swagger";
                });
                app.UseDeveloperExceptionPage();
                app.UseCors(this.MyAllowSpecificOrigins);
            }
            else
            {
                app.UseSpaStaticFiles();
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");

                endpoints.MapHub<TransactionHub>("/signalr");
            });

            if (!env.IsDevelopment())
            {
                app.UseSpa(spa =>
                {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";
                    //if (env.IsDevelopment())
                    //{
                    //    spa.UseAngularCliServer(npmScript: "start");
                    //}
                });
            }
        }
    }
}

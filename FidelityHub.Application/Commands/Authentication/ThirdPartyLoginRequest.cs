using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FidelityHub.Application.Helpers;
using FidelityHub.Application.Interfaces;
using FidelityHub.Application.Models.Authentication;
using FidelityHub.Application.Models.Authentication.ThirdParty;
using FidelityHub.Database.Entities.UsrSchema;
using MediatR;

namespace FidelityHub.Application.Commands.Authentication
{
    public class ThirdPartyLoginRequest 
    {
        public class AuthorizeThirdParty : IRequest<AccessToken>
        {
            public AspNetUser User { get; }

            public AuthorizeThirdParty(AspNetUser user)
            {
                this.User = user;
            }
        }

        public class AuthorizeThirdPartyRequestHandler : IRequestHandler<AuthorizeThirdParty, AccessToken>
        {
            private IDboSchemaDataReader Reader { get; }
            private IJwtFactory JwtFactory { get; }
            private ITokenFactory TokenFactory { get; }

            public AuthorizeThirdPartyRequestHandler(IDboSchemaDataReader reader, IJwtFactory jwtFactory, ITokenFactory tokenFactory)
            {
                this.Reader = reader;
                this.JwtFactory = jwtFactory;
                this.TokenFactory = tokenFactory;
            }

            public async Task<AccessToken> Handle(AuthorizeThirdParty request, CancellationToken cancellationToken)
            {
                if (request.User != null)
                {
                    var refreshToken = this.TokenFactory.GenerateToken();
                    await this.Reader.SetRefreshTokenForUser(request.User, refreshToken, null);
                    await this.Reader.UpdateUserForRefreshToken(request.User);
                    var accessToken = await this.JwtFactory.GenerateEncodedToken(request.User);
                    accessToken.RefreshToken = refreshToken;
                    return accessToken;
                }

                throw new Exception("Error generating token!");
            }
        }

        public class Google : IRequest<AspNetUser>
        {
            public TokenRequestModel Model { get; }
            public string RemoteHostIP { get; }

            public Google(TokenRequestModel model, string remoteHostIp)
            {
                this.Model = model;
                this.RemoteHostIP = remoteHostIp;
            }
        }

        public class GoogleRequestHandler : IRequestHandler<Google, AspNetUser>
        {
            private string GoogleAPIUrl = "https://www.googleapis.com/oauth2/v3/tokeninfo?id_token=";
            private string GoogleCertsUrl = "https://www.googleapis.com/oauth2/v3/certs";

            private IDboSchemaDataReader Reader { get; }
            private IJwtFactory JwtFactory { get; }
            private ITokenFactory TokenFactory { get; }

            public GoogleRequestHandler(IDboSchemaDataReader reader, IJwtFactory jwtFactory, ITokenFactory tokenFactory)
            {
                this.Reader = reader;
                this.JwtFactory = jwtFactory;
                this.TokenFactory = tokenFactory;
            }

            private bool IsGoogleIdTokenValid(GoogleCertResponse response, GoogleIdResponse id)
            {
                return response.Keys.FindIndex(x => x.kid == id.kid) != -1;
            }

            public async Task<AspNetUser> Handle(Google request, CancellationToken cancellationToken)
            {
                GoogleIdResponse idResponse = null;
                GoogleCertResponse certResponse = null;

                using (CustomHttpClient<GoogleIdResponse> client = new CustomHttpClient<GoogleIdResponse>(GoogleAPIUrl))
                {
                    idResponse = client.QueryApi("?id_token=" + request.Model.IdToken).Result;
                }

                using (CustomHttpClient<GoogleCertResponse> client = new CustomHttpClient<GoogleCertResponse>(GoogleCertsUrl))
                {
                    certResponse = client.QueryApi().Result;
                }

                if (!string.IsNullOrWhiteSpace(idResponse.email) && this.IsGoogleIdTokenValid(certResponse, idResponse))
                {
                    return await this.Reader.GetUserByEmail(idResponse.email);
                }

                throw new Exception("Invalid Google Token!");
            }
        }

        public class Facebook : IRequest<AspNetUser>
        {
            public TokenRequestModel Model { get; }
            public string RemoteHostIP { get; }

            public Facebook(TokenRequestModel model, string remoteHostIp)
            {
                this.Model = model;
                this.RemoteHostIP = remoteHostIp;
            }
        }

        public class FacebookRequestHandler : IRequestHandler<Facebook, AspNetUser>
        {
            private Dictionary<string, string> Params = new Dictionary<string, string>{
                { "client_id", "321531179059493" },
                { "client_secret" , "85db42f4f2ad1e21318d8ce128de35f1" },
                { "grant_type" , "client_credentials" }
            };

            private string FacebookAPIUrl = "https://graph.facebook.com/";
            private string FacebookCertsUrl = "https://graph.facebook.com/oauth/";

            private IDboSchemaDataReader Reader { get; }

            public FacebookRequestHandler(IDboSchemaDataReader reader)
            {
                this.Reader = reader;
            }

            private bool IsFacebookIdTokenValid(FacebookCertResponse response, FacebookIdResponse id)
            {
                var app_id = response.access_token.Substring(0, response.access_token.IndexOf('|'));

                return id.data.is_valid && (app_id.Trim() == id.data.app_id);
            }

            public async Task<AspNetUser> Handle(Facebook request, CancellationToken cancellationToken)
            {
                FacebookIdResponse idResponse = null;
                FacebookCertResponse certResponse = null;

                using (CustomHttpClient<FacebookCertResponse> client = new CustomHttpClient<FacebookCertResponse>(FacebookCertsUrl))
                {
                    string queryParams = $"access_token?client_id={this.Params["client_id"]}&client_secret={this.Params["client_secret"]}&grant_type={this.Params["grant_type"]}";
                    certResponse = client.QueryApi(queryParams).Result;
                }

                using (CustomHttpClient<FacebookIdResponse> client = new CustomHttpClient<FacebookIdResponse>(FacebookAPIUrl))
                {
                    string queryParams = $"debug_token?input_token={request.Model.IdToken}&access_token={certResponse.access_token}";
                    idResponse = client.QueryApi(queryParams).Result;
                }

                if (this.IsFacebookIdTokenValid(certResponse, idResponse))
                {
                    return await this.Reader.GetUserByEmail(request.Model.Email);
                }

                throw new Exception("Invalid Facebook Token!");
            }
        }
    }
}

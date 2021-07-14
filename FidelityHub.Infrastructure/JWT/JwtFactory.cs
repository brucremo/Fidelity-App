using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using FidelityHub.Infrastructure.Database;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using FidelityHub.Application.Interfaces;
using FidelityHub.Application.Models.Authentication;
using FidelityHub.Database.Entities.UsrSchema;
using System.Linq;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace FidelityHub.Infrastructure.JWT
{
    public class JwtFactory : IJwtFactory
    {
        private readonly IJwtTokenHandler _jwtTokenHandler;
        private readonly JwtIssuerOptions _jwtOptions;

        public JwtFactory(IJwtTokenHandler jwtTokenHandler, IOptions<JwtIssuerOptions> jwtOptions)
        {
            _jwtTokenHandler = jwtTokenHandler;
            _jwtOptions = jwtOptions.Value;
            ThrowIfInvalidOptions(_jwtOptions);
        }

        public async Task<AccessToken> GenerateEncodedToken(AspNetUser user)
        {
            var identity = GenerateClaimsIdentity(user.Id.ToString(), user.UserName, user.UserTypeId.Value);

            List<Claim> claims = new List<Claim>();

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.UserName));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64));

            identity.Claims.ToList().ForEach(x => claims.Add(x));

            // Create the JWT security token and encode it.
            var jwt = new JwtSecurityToken(
                _jwtOptions.Issuer,
                _jwtOptions.Audience,
                claims,
                //DateTime.Now,
                _jwtOptions.NotBefore,
                //DateTime.Now.AddSeconds(60),
                _jwtOptions.Expiration,
                _jwtOptions.SigningCredentials);
          
            return new AccessToken(_jwtTokenHandler.WriteToken(jwt), (int)_jwtOptions.ValidFor.TotalSeconds, null);
        }

        private static ClaimsIdentity GenerateClaimsIdentity(string id, string userName, int userTypeId)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(Constants.Strings.JwtClaimIdentifiers.Id, id));
            claims.Add(new Claim(Constants.Strings.JwtClaimIdentifiers.Rol, Constants.Strings.JwtClaims.AuthenticatedUser));

            switch (userTypeId)
            {
                case 1:
                    claims.Add(new Claim(Constants.Strings.JwtClaimIdentifiers.Rol, Constants.Strings.JwtClaims.AdminAccess));
                    claims.Add(new Claim(Constants.Strings.JwtClaimIdentifiers.Rol, Constants.Strings.JwtClaims.SupportAccess));
                    claims.Add(new Claim(Constants.Strings.JwtClaimIdentifiers.Rol, Constants.Strings.JwtClaims.VendorAdminAccess));
                    claims.Add(new Claim(Constants.Strings.JwtClaimIdentifiers.Rol, Constants.Strings.JwtClaims.CustomerAccess));
                    claims.Add(new Claim(Constants.Strings.JwtClaimIdentifiers.Rol, Constants.Strings.JwtClaims.VendorAccess));
                    break;
                case 2:
                    claims.Add(new Claim(Constants.Strings.JwtClaimIdentifiers.Rol, Constants.Strings.JwtClaims.SupportAccess));
                    break;
                case 3:
                    claims.Add(new Claim(Constants.Strings.JwtClaimIdentifiers.Rol, Constants.Strings.JwtClaims.VendorAdminAccess));
                    claims.Add(new Claim(Constants.Strings.JwtClaimIdentifiers.Rol, Constants.Strings.JwtClaims.VendorAccess));
                    break;
                case 4:
                    claims.Add(new Claim(Constants.Strings.JwtClaimIdentifiers.Rol, Constants.Strings.JwtClaims.CustomerAccess));
                    break;
                case 5:
                    claims.Add(new Claim(Constants.Strings.JwtClaimIdentifiers.Rol, Constants.Strings.JwtClaims.VendorAccess));
                    break;
            }

            return new ClaimsIdentity(new GenericIdentity(userName, "Token"), claims);
        }

        /// <returns>Date converted to seconds since Unix epoch (Jan 1, 1970, midnight UTC).</returns>
        private static long ToUnixEpochDate(DateTime date)
          => (long)Math.Round((date.ToUniversalTime() -
                               new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                              .TotalSeconds);

        private static void ThrowIfInvalidOptions(JwtIssuerOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            if (options.ValidFor <= TimeSpan.Zero)
            {
                throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(JwtIssuerOptions.ValidFor));
            }

            if (options.SigningCredentials == null)
            {
                throw new ArgumentNullException(nameof(JwtIssuerOptions.SigningCredentials));
            }

            if (options.JtiGenerator == null)
            {
                throw new ArgumentNullException(nameof(JwtIssuerOptions.JtiGenerator));
            }
        }
    }
}

using FidelityHub.Application.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace FidelityHub.Infrastructure.JWT
{
    public class JwtTokenValidator : IJwtTokenValidator
    {
        private IJwtTokenHandler JwtTokenHandler { get; }

        public JwtTokenValidator(IJwtTokenHandler jwtTokenHandler)
        {
            JwtTokenHandler = jwtTokenHandler;
        }

        public ClaimsPrincipal GetPrincipalFromToken(string token, string signingKey)
        {
            return JwtTokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey)),
                ValidateLifetime = false // we check expired tokens here
            });
        }
    }
}

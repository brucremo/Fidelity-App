using FidelityHub.Database.Entities.UsrSchema;
using System;
using System.Collections.Generic;
using System.Text;

namespace FidelityHub.Application.Models.Authentication
{
    public class AccessToken
    {
        public string Token { get; }
        public string RefreshToken { get; set; }
        public int ExpiresIn { get; }

        public AccessToken(string token, int expiresIn, string refreshToken)
        {
            Token = token;
            ExpiresIn = expiresIn;
            RefreshToken = refreshToken;
        }
    }
}

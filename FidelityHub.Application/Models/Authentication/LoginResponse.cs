using System;
using System.Collections.Generic;
using System.Text;

namespace FidelityHub.Application.Models.Authentication
{
    public class LoginResponse
    {
        public AccessToken AccessToken { get; }
        public string RefreshToken { get; }
    }
}

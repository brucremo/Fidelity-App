using System;
using System.Collections.Generic;
using System.Text;

namespace FidelityHub.Application.Models.Authentication
{
    public class RefreshTokenModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}

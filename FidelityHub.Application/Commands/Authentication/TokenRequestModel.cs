using System;
using System.Collections.Generic;
using System.Text;

namespace FidelityHub.Application.Commands.Authentication
{
    public class TokenRequestModel
    {
        public string ThirdParty { get; set; }
        public string IdToken { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
    }
}

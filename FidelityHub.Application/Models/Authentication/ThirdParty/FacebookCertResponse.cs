using System;
using System.Collections.Generic;
using System.Text;

namespace FidelityHub.Application.Models.Authentication.ThirdParty
{
    public class FacebookCertResponse
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
    }
}

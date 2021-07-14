using System;
using System.Collections.Generic;
using System.Text;

namespace FidelityHub.Application.Models.Authentication.ThirdParty
{
    public class GoogleCertResponse
    {
        public List<GoogleCertKey> Keys { get; set; }
    }

    public class GoogleCertKey
    {
        public string n { get; set; }
        public string alg { get; set; }
        public string kty { get; set; }
        public string e { get; set; }
        public string use { get; set; }
        public string kid { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace FidelityHub.Application.Models.Authentication.ThirdParty
{
    public class FacebookIdResponse
    {
        public FacebookIdData data { get; set; }
    }

    public class FacebookIdData
    {
        public string app_id { get; set; }
        public string type { get; set; }
        public string application { get; set; }
        public string data_access_expires_at { get; set; }
        public string expires_at { get; set; }
        public bool is_valid { get; set; }
        public IEnumerable<string> scopes { get; set; }
        public string user_id { get; set; }
    }
}

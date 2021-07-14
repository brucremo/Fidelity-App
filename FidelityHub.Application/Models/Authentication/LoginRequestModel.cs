using System;
using System.Collections.Generic;
using System.Text;

namespace FidelityHub.Application.Models.Authentication
{
    public class LoginRequestModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace FidelityHub.Communication.Email
{
    public class SmtpOptions
    {
        public string SenderTitle { get; set; }
        public string SenderAddress { get; set; }
        public string SenderPassword { get; set; }
        public bool EnableSSL { get; set; }
        public string SMTPHost { get; set; }
        public int Port { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace FidelityHub.Application.Models.Mail
{
    public class MailModel
    {
        public string EmailAddress { get; set; }

        public MailModel(string emailAddress)
        {
            this.EmailAddress = emailAddress;
        }
    }
}

using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace FidelityHub.Communication.Email
{
    public class CustomSmtpClient : SmtpClient
    {
        private int Attempts { get; set; }
        private string SenderAddress { get; }
        private string SenderTitle { get; }

        public CustomSmtpClient(IOptions<SmtpOptions> options) : base()
        {
            var config = options.Value;
            this.Host = config.SMTPHost;
            this.Port = config.Port;
            this.EnableSsl = config.EnableSSL;
            this.DeliveryMethod = SmtpDeliveryMethod.Network;
            this.UseDefaultCredentials = false;
            this.SenderAddress = config.SenderAddress;
            this.SenderTitle = config.SenderTitle;
            this.Credentials = new NetworkCredential(config.SenderAddress, config.SenderPassword);
        }

        public async Task<bool> SendMessage(MailMessage message)
        {
            this.Attempts = 0;
            message.From = new MailAddress(this.SenderAddress);
            try
            {
                await this.SendMailAsync(message);
                return true;
            }
            catch(SmtpException ex)
            {
                Console.WriteLine(ex);
                var success = false;

                while(this.Attempts < 4 && !success)
                {
                    this.Attempts++;
                    success = await this.SendMessage(message);
                }

                return false;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
    }
}

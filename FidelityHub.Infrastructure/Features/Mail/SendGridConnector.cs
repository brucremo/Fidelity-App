using FidelityHub.Application.Interfaces;
using Microsoft.VisualBasic;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Net;
using System.Threading.Tasks;
using FidelityHub.Application.Helpers;
using Microsoft.Extensions.Options;

namespace FidelityHub.Infrastructure.Features.Mail
{
    public class SendGridConnector : ISendGridConnector
    {
        private SendGridClient Client { get; }
        private IRefSchemaDataReader Reader { get; }

        public SendGridConnector(IRefSchemaDataReader reader, IOptions<SendGridClientOptions> options)
        {
            var option = options.Value;
            this.Reader = reader;
            this.Client = new SendGridClient(option.ApiKey);
        }

        public async Task<HttpStatusCode> SendContactRequest(string emailAddress)
        {
            var msg = new SendGridMessage();

            msg.SetFrom(new EmailAddress("contato.fidelityhub@gmail.com", "FidelityHub - Pedido de Informação"));
            msg.AddTo(new EmailAddress("contato.fidelityhub@gmail.com"));

            msg.SetSubject("FidelityHub - Pedido de Informação");
            msg.AddContent(MimeType.Text, "Entrar em contato atraves de " + emailAddress);
            var response = await this.Client.SendEmailAsync(msg);

            await this.Reader.RegisterContactRequest(DateTime.UtcNow, emailAddress, response.StatusCode.ToString(), response.StatusCode.IsStatusCodeSuccessful());

            return response.StatusCode;
        }
    }
}

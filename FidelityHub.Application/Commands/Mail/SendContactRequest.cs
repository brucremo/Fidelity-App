using FidelityHub.Application.Models.Mail;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using SendGrid;
using FidelityHub.Application.Interfaces;
using System.Net;

namespace FidelityHub.Application.Commands.Mail
{
    public class SendContactRequest : MailModel, IRequest<HttpStatusCode>
    {
        public SendContactRequest(string emailAddress) : base(emailAddress)
        {
            this.EmailAddress = emailAddress;
        }
    }

    public class SendContactRequestHandler : IRequestHandler<SendContactRequest, HttpStatusCode>
    {
        public ISendGridConnector Connector { get; }

        public SendContactRequestHandler(ISendGridConnector connector)
        {
            this.Connector = connector;
        }

        public async Task<HttpStatusCode> Handle(SendContactRequest request, CancellationToken cancellationToken)
        {
            return await this.Connector.SendContactRequest(request.EmailAddress);
        }
    }
}

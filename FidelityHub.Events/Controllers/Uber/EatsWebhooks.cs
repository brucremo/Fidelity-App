using FidelityHub.Application.Helpers.Api;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace FidelityHub.Events.Controllers.Uber
{
    public class EatsWebhooks : BaseController
    {
        public EatsWebhooks(IMediator mediator) : base(mediator)
        {

        }

        [Route("order-received")]
        [HttpPost]
        public async Task<HttpStatusCode> ProcessOrderReceivedWebhook([FromQuery] string emailAddress)
        {
            try
            {
                var commandResult = await this.Mediator.Send(new SendContactRequest(emailAddress));
                return new RequestResult(commandResult, "Email enviado! Breve entraremos em contato", null);
            }
            catch (Exception ex)
            {
                return new RequestResult(HttpStatusCode.InternalServerError, ex.Message, null);
            }
        }
    }
}

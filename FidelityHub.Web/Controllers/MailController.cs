using System;
using System.Net;
using System.Threading.Tasks;
using FidelityHub.Application.Helpers.Api;
using FidelityHub.Application.Commands.Mail;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FidelityHub.Web.Controllers
{
    public class MailController : BaseController
    {
        public MailController(IMediator mediator) : base(mediator)
        {

        }

        [Route("contact-request")]
        [HttpGet]
        public async Task<RequestResult> SendContactRequestMail([FromQuery] string emailAddress)
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

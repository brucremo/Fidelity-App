using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FidelityHub.Application.Commands.Registration;
using FidelityHub.Application.Models.Registration;
using FidelityHub.Application.Helpers.Api;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FidelityHub.Web.Controllers
{
    public class RegistrationController : BaseController
    {
        public RegistrationController(IMediator mediator): base(mediator)
        {
        }

        [Route("register-vendor")]
        [HttpPost]
        public async Task<RequestResult> RegisterVendorRequest([FromBody] VendorRegistrationModel vendorModel)
        {
            try
            {
                var commandResult = await this.Mediator.Send(new RegisterVendorRequest(vendorModel));
                return new RequestResult(commandResult, "Usuario Registrado com Sucesso!", null);
            }
            catch (Exception ex)
            {
                return new RequestResult(HttpStatusCode.InternalServerError, ex.Message, null);
            }
        }

        [Route("register-customer")]
        [HttpPost]
        public async Task<RequestResult> RegisterCustomerRequest([FromBody] CustomerRegistrationModel customerModel)
        {
            try
            {
                var commandResult = await this.Mediator.Send(new RegisterCustomerRequest(customerModel));
                return new RequestResult(commandResult, "Usuario Registrado com Sucesso!", null);
            }
            catch (Exception ex)
            {
                return new RequestResult(HttpStatusCode.InternalServerError, ex.Message, null);
            }
        }
    }
}

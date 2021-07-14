using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FidelityHub.Application.Commands.Authentication;
using FidelityHub.Application.Helpers.Api;
using MediatR;
using FidelityHub.Application.Models.Authentication;
using Microsoft.AspNetCore.Authorization;
using FidelityHub.Application.Commands.Registration;
using FidelityHub.Application.Models.Registration;
using System.Net;
using FidelityHub.Database.Entities.UsrSchema;

namespace FidelityHub.Web.Controllers
{
    public class ThirdPartyAuthenticationController : BaseController
    {
        public ThirdPartyAuthenticationController(IMediator mediator) : base(mediator)
        {

        }

        [HttpPost("tokensignin")]
        public async Task<AccessToken> ThirdPartyLogin([FromBody] TokenRequestModel tokenRequest)
        {
            AspNetUser thirdPartyUser = null;

            if(tokenRequest.ThirdParty == "Google")
            {
                thirdPartyUser = await this.Mediator.Send(new ThirdPartyLoginRequest.Google(tokenRequest, Request.HttpContext.Connection.RemoteIpAddress?.ToString()));
            }
            else
            {
                thirdPartyUser = await this.Mediator.Send(new ThirdPartyLoginRequest.Facebook(tokenRequest, Request.HttpContext.Connection.RemoteIpAddress?.ToString()));
            }
                
            
            if (thirdPartyUser == null)
            {
                thirdPartyUser = await this.Mediator.Send(new RegisterThirdPartyRequest(new ThirdPartyRegistrationModel
                {
                    Email = tokenRequest.Email,
                    Name = tokenRequest.Name,
                    PhotoUrl = tokenRequest.PhotoUrl
                }));
            }

            return await this.Mediator.Send(new ThirdPartyLoginRequest.AuthorizeThirdParty(thirdPartyUser));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FidelityHub.Application.Commands.Authentication;
using FidelityHub.Application.Models.Authentication;
using FidelityHub.Application.Helpers.Api;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace FidelityHub.Web.Controllers
{
    public class AuthenticationController : BaseController
    {
        private AuthSettings AuthSettings { get; }

        public AuthenticationController(IMediator mediator, IOptions<AuthSettings> authSettings) : base(mediator)
        {
            this.AuthSettings = authSettings.Value;
        }

        [HttpPost("login")]
        public async Task<AccessToken> Login([FromBody] LoginRequestModel request)
        {
            return await this.Mediator.Send(new LoginRequest(request, Request.HttpContext.Connection.RemoteIpAddress?.ToString()));
        }

        [HttpPost("refreshtoken")]
        public async Task<AccessToken> RefreshToken([FromBody] RefreshTokenModel request)
        {
            return await this.Mediator.Send(new RefreshTokenRequest(request, Request.HttpContext.Connection.RemoteIpAddress?.ToString(), this.AuthSettings.SecretKey));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FidelityHub.Application.Commands.Account;
using FidelityHub.Application.Commands.Authentication;
using FidelityHub.Application.Commands.Mail;
using FidelityHub.Application.Models.Account;
using FidelityHub.Application.Models.Authentication;
using FidelityHub.Application.Queries.Account;
using FidelityHub.Application.Helpers.Api;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FidelityHub.Web.Controllers
{
    public static class AccountExtensions
    {
        public static string ReplaceTokenCharacters(this string token)
        {
            token = token.Replace('|', '/');
            token = token.Replace(' ', '+');
            return token;
        }
    }

    public class AccountController : BaseController
    {
        public AccountController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("forgot-password")]
        public async Task<HttpStatusCode> ForgotPassword([FromQuery] string email)
        {
            return await this.Mediator.Send(new ResetPasswordRequest(email));
        }

        [HttpGet("validate-reset-token")]
        public async Task<bool> IsResetTokenValid([FromQuery] string token)
        {
            try
            {
                return await this.Mediator.Send(new QueryResetTokenValid(token.ReplaceTokenCharacters()));
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        [HttpPost("reset-forgotten-password")]
        public async Task<bool> ResetForgottenPassword(ResetForgottenPasswordModel model)
        {
            model.ResetToken = model.ResetToken.ReplaceTokenCharacters();
            return await this.Mediator.Send(new ResetForgottenPasswordRequest(model));
        }
    }
}

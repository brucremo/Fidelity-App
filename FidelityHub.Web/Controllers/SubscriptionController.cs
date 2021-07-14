using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FidelityHub.Application.Queries;
using FidelityHub.Database.Entities.UsrSchema;
using FidelityHub.Application.Helpers.Api;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FidelityHub.Web.Controllers
{
    public class SubscriptionController : BaseController
    {
        public SubscriptionController(IMediator mediator) : base(mediator)
        {
        }

        [Route("")]
        [HttpGet]
        public async Task<List<Subscription>> GetSubscriptions()
        {
            try
            { 
                return await this.Mediator.Send(new QuerySubscriptions());
            }
            catch (Exception ex)
            {
                return new List<Subscription>();
            }
        }

        [Route("email-exists")]
        [HttpGet]
        public async Task<bool> GetEmailExists([FromQuery] string email)
        {
            return await this.Mediator.Send(new QueryEmailExists(email));
        }

        [Route("username-exists")]
        [HttpGet]
        public async Task<bool> GetUserNameExists([FromQuery] string userName)
        {
            return await this.Mediator.Send(new QueryUserNameExists(userName));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FidelityHub.Application.Commands.Vendor;
using FidelityHub.Application.Models.Shared;
using FidelityHub.Application.Models.Vendor;
using FidelityHub.Application.Queries.Vendor;
using FidelityHub.Application.SignalR.Hubs;
using FidelityHub.Database.Entities.AppSchema;
using FidelityHub.Application.Helpers.Api;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace FidelityHub.Web.Controllers
{
    [Authorize(Policy = "Vendor")]
    public class VendorController : BaseController
    {
        private IHubContext<TransactionHub> Hub { get; }

        public VendorController(IMediator mediator, IHubContext<TransactionHub> hub) : base(mediator)
        {
            this.Hub = hub;
        }

        // ----- Promotions -----
        [Route("promotions-unit")]
        [HttpGet]
        public async Task<IEnumerable<Promotion>> GetPromotionsByUnit([FromQuery] int id)
        {
            return await this.Mediator.Send(new QueryPromotions(id));
        }

        [Route("register-sale")]
        [HttpPost]
        public async Task<RequestResult> RegisterSale([FromBody] SaleRequestModel saleRequest)
        {
            try
            {
                var commandResult = await this.Mediator.Send(new SaleRequest.Create(saleRequest));

                await this.Hub.Clients.Client(commandResult.UserId).SendAsync("saleconfirmation", commandResult);

                if(commandResult.AmountUntilReward <= 0)
                {
                    await this.Mediator.Send(new RegisterRewardRequest(saleRequest));
                }

                return new RequestResult(HttpStatusCode.OK, "Venda registrada!", commandResult);
            }
            catch (Exception ex)
            {
                return new RequestResult(HttpStatusCode.InternalServerError, ex.Message, null);
            }
        }
    }
}

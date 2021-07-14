using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FidelityHub.Application.Commands.Vendor;
using FidelityHub.Application.Helpers.Api;
using FidelityHub.Application.Models.Customer;
using FidelityHub.Application.Models.Promotion;
using FidelityHub.Application.Models.Sale;
using FidelityHub.Application.Models.Shared;
using FidelityHub.Application.Models.Vendor;
using FidelityHub.Application.Queries.Vendor;
using FidelityHub.Database.Entities.AppSchema;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FidelityHub.Web.Controllers
{
    [Authorize(Policy = "VendorAdmin")]
    public class VendorAdminController : BaseController
    {
        public VendorAdminController(IMediator mediator) : base(mediator)
        {
        }

        // ----- Sales -----
        [Route("sales")]
        [HttpGet]
        public async Task<SalesViewModel> GetSales([FromQuery] int vendorUnitId, DateTime from, DateTime to)
        {
            return await this.Mediator.Send(new Query.AllSales(vendorUnitId, from, to));
        }

        [Route("sales-chart")]
        [HttpGet]
        public async Task<IEnumerable<GraphModel>> GetSalesChart([FromQuery] int vendorUnitId, DateTime from, DateTime to)
        {
            return await this.Mediator.Send(new Query.SalesChart(vendorUnitId, from, to));
        }

        [Route("edit-sales")]
        [HttpPut]
        public async Task<IEnumerable<PromotionSale>> UpdateSales([FromBody] IEnumerable<PromotionSale> sales)
        {
            return await this.Mediator.Send(new SaleRequest.Update(sales));
        }

        [Route("delete-sales")]
        [HttpPost]
        public async Task<IEnumerable<PromotionSale>> DeleteSales([FromBody] IEnumerable<PromotionSale> sales)
        {
            return await this.Mediator.Send(new SaleRequest.Delete(sales));
        }

        // ----- Unit -----
        [Route("units")]
        [HttpGet]
        public async Task<List<VendorUnitViewModel>> GetVendorUnits()
        {
            return await this.Mediator.Send(new QueryVendorUnits(this.User.Identity.Name));
        }

        [Route("unit")]
        [HttpPost]
        public async Task<RequestResult> CreateVendorUnit([FromBody] VendorUnitViewModel viewModel)
        {
            try
            {
                var commandResult = await this.Mediator.Send(new VendorUnitRequest.Create(viewModel));
                return new RequestResult(commandResult, "Unidade criada com sucesso!", null);
            }
            catch (Exception ex)
            {
                return new RequestResult(HttpStatusCode.InternalServerError, ex.Message, null);
            }
        }

        [Route("unit")]
        [HttpPut]
        public async Task<RequestResult> UpdateVendorUnit([FromBody] VendorUnitViewModel viewModel)
        {
            try
            {
                var commandResult = await this.Mediator.Send(new VendorUnitRequest.Update(viewModel));
                return new RequestResult(commandResult, "Unidade atualizada com sucesso!", null);
            }
            catch (Exception ex)
            {
                return new RequestResult(HttpStatusCode.InternalServerError, ex.Message, null);
            }
        }

        // ---- Customers ----
        [Route("customers")]
        [HttpGet]
        public async Task<IEnumerable<CustomerViewModel>> GetAllCustomersEnrolled([FromQuery] int? vendorUnitId = null, int? promotionId = null)
        {
            return await this.Mediator.Send(new QueryCustomers.EnrolledBy(vendorUnitId, promotionId));
        }

        // ---- Promotion ----
        [Route("promotions-vendor")]
        [HttpGet]
        public async Task<IEnumerable<Promotion>> GetPromotionsByVendor()
        {
            return await this.Mediator.Send(new QueryPromotions(this.User.Identity.Name));
        }

        [Route("promotion")]
        [HttpPost]
        public async Task<RequestResult> CreatePromotion([FromBody] PromotionViewModel viewModel)
        {
            try
            {
                var commandResult = await this.Mediator.Send(new PromotionRequest.Create(viewModel));
                return new RequestResult(commandResult, "Promoção criada com sucesso!", null);
            }
            catch (Exception ex)
            {
                return new RequestResult(HttpStatusCode.InternalServerError, ex.Message, null);
            }
        }

        [Route("promotion")]
        [HttpPut]
        public async Task<RequestResult> UpdatePromotion([FromQuery] PromotionViewModel viewModel)
        {
            try
            {
                var commandResult = await this.Mediator.Send(new PromotionRequest.Update(viewModel));
                return new RequestResult(commandResult, "Promoção atualizada com sucesso!", null);
            }
            catch (Exception ex)
            {
                return new RequestResult(HttpStatusCode.InternalServerError, ex.Message, null);
            }
        }
    }
}

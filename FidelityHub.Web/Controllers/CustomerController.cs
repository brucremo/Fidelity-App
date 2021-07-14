using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FidelityHub.Application.Models.Customer;
using FidelityHub.Application.Models.Sale;
using FidelityHub.Application.Models.Shared;
using FidelityHub.Application.Models.Vendor;
using FidelityHub.Application.Queries.Customer;
using FidelityHub.Database.Entities.AppSchema;
using FidelityHub.Application.Helpers.Api;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FidelityHub.Web.Controllers
{
    [Authorize(Policy = "Customer")]
    public class CustomerController : BaseController
    {
        public CustomerController(IMediator mediator) : base(mediator)
        {

        }

        // ----- Vendors -----
        [Route("vendors")]
        [HttpGet]
        public async Task<IEnumerable<VendorViewModel>> GetEnrolledVendors([FromQuery] string customerId)
        {
            return await this.Mediator.Send(new QueryVendors.Enrolled(customerId));
        }

        // ----- Promotions -----
        [Route("promotions")]
        [HttpGet]
        public async Task<IEnumerable<CustomerPromotionDashboardViewModel>> GetEnrolledPromotions([FromQuery] string customerId)
        {
            return await this.Mediator.Send(new QueryPromotions.Enrolled(customerId));
        }

        [Route("promotion/purchases")]
        [HttpGet]
        public async Task<SalesModel> GetEnrolledPromotionSales([FromQuery] string customerId, int promotionId)
        {
            return await this.Mediator.Send(new QueryPromotions.Sales(customerId, promotionId));
        }
    }
}

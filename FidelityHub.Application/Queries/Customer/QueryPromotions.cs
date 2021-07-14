using FidelityHub.Application.Interfaces;
using FidelityHub.Application.Models.Customer;
using FidelityHub.Application.Models.Sale;
using FidelityHub.Application.Models.Shared;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FidelityHub.Application.Queries.Customer
{
    public class QueryPromotions
    {
        public class Enrolled : IRequest<IEnumerable<CustomerPromotionDashboardViewModel>>
        {
            public string CustomerId { get; }

            public Enrolled(string customerId)
            {
                this.CustomerId = customerId;
            }
        }

        public class EnrolledPromotionsHandler : IRequestHandler<Enrolled, IEnumerable<CustomerPromotionDashboardViewModel>>
        {
            public IAppSchemaDataReader Reader { get; }

            public EnrolledPromotionsHandler(IAppSchemaDataReader reader)
            {
                this.Reader = reader;
            }

            public async Task<IEnumerable<CustomerPromotionDashboardViewModel>> Handle(Enrolled request, CancellationToken cancellationToken)
            {
                return await this.Reader.GetCustomerEnrolledPromotions(request.CustomerId);
            }
        }

        public class Sales : IRequest<SalesModel>
        {
            public string CustomerId { get; }
            public int PromotionId { get; }

            public Sales(string customerId, int promotionId)
            {
                this.CustomerId = customerId;
                this.PromotionId = promotionId;
            }
        }

        public class SalesPromotionHandler : IRequestHandler<Sales, SalesModel>
        {
            public IAppSchemaDataReader Reader { get; }

            public SalesPromotionHandler(IAppSchemaDataReader reader)
            {
                this.Reader = reader;
            }

            public async Task<SalesModel> Handle(Sales request, CancellationToken cancellationToken)
            {
                return await this.Reader.GetCustomerSalesByPromotion(request.CustomerId, request.PromotionId);
            }
        }
    }
}


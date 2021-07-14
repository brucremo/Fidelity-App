using FidelityHub.Application.Interfaces;
using FidelityHub.Application.Models.Customer;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FidelityHub.Application.Queries.Vendor
{
    public class QueryCustomers
    {
        public class EnrolledBy : IRequest<IEnumerable<CustomerViewModel>>
        {
            public int? VendorUnitId { get; }
            public int? PromotionId { get; }

            public EnrolledBy(int? vendorUnitId = null, int? promotionId = null)
            {
                this.VendorUnitId = vendorUnitId;
                this.PromotionId = promotionId;
            }
        }

        public class QueryCustomersEnrolledByHandler : IRequestHandler<EnrolledBy, IEnumerable<CustomerViewModel>>
        {
            public IAppSchemaDataReader Reader { get; }

            public QueryCustomersEnrolledByHandler(IAppSchemaDataReader reader)
            {
                this.Reader = reader;
            }

            public async Task<IEnumerable<CustomerViewModel>> Handle(EnrolledBy request, CancellationToken cancellationToken)
            {
                if (!request.PromotionId.HasValue && request.VendorUnitId.HasValue)
                {
                    return await this.Reader.GetAllCustomersEnrolledByVendorUnit(request.VendorUnitId.Value);
                }
                else if (request.PromotionId.HasValue && !request.VendorUnitId.HasValue)
                {
                    return await this.Reader.GetAllCustomersEnrolledByPromotion(request.PromotionId.Value);
                }
                else if (request.PromotionId.HasValue && request.VendorUnitId.HasValue)
                {
                    return await this.Reader.GetAllCustomersEnrolledByVendorUnitAndPromotion(request.VendorUnitId.Value, request.PromotionId.Value);
                }
                else
                {
                    throw new Exception("Metodo nao existente");
                }
            }
        }
    }
}

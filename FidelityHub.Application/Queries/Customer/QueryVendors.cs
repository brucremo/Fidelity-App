using FidelityHub.Application.Interfaces;
using FidelityHub.Application.Models.Vendor;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FidelityHub.Application.Queries.Customer
{
    public class QueryVendors
    {
        public class Enrolled : IRequest<IEnumerable<VendorViewModel>>
        {
            public string CustomerId { get; }

            public Enrolled(string customerId)
            {
                this.CustomerId = customerId;
            }
        }

        public class EnrolledVendorsHandler : IRequestHandler<Enrolled, IEnumerable<VendorViewModel>>
        {
            public IAppSchemaDataReader Reader { get; }

            public EnrolledVendorsHandler(IAppSchemaDataReader reader)
            {
                this.Reader = reader;
            }

            public async Task<IEnumerable<VendorViewModel>> Handle(Enrolled request, CancellationToken cancellationToken)
            {
                return await this.Reader.GetCustomerEnrolledVendors(request.CustomerId);
            }
        }
    }
}

using FidelityHub.Application.Interfaces;
using FidelityHub.Application.Models.Vendor;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FidelityHub.Application.Queries.Vendor
{
    public  class QueryVendorUnits : IRequest<List<VendorUnitViewModel>>
    {
        public string User { get; }

        public QueryVendorUnits(string user)
        {
            this.User = user;
        }
    }

    public class QueryVendorUnitsHandler : IRequestHandler<QueryVendorUnits, List<VendorUnitViewModel>>
    {
        public IAppSchemaDataReader Reader { get; }

        public QueryVendorUnitsHandler(IAppSchemaDataReader reader)
        {
            this.Reader = reader;
        }

        public async Task<List<VendorUnitViewModel>> Handle(QueryVendorUnits request, CancellationToken cancellationToken)
        {
            return await this.Reader.GetVendorUnits(request.User);
        }
    }
}

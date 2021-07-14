using FidelityHub.Application.Interfaces;
using FidelityHub.Database.Entities.AppSchema;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FidelityHub.Application.Queries.Vendor
{
    public class QueryPromotions : IRequest<IEnumerable<Promotion>>
    {
        public int Id { get; }
        public string User { get; }

        public QueryPromotions(int id)
        {
            this.Id = id;
        }

        public QueryPromotions(string user)
        {
            this.User = user;
        }
    }

    public class QueryPromotionsHandler : IRequestHandler<QueryPromotions, IEnumerable<Promotion>>
    {
        public IAppSchemaDataReader Reader { get; }

        public QueryPromotionsHandler(IAppSchemaDataReader reader)
        {
            this.Reader = reader;
        }

        public async Task<IEnumerable<Promotion>> Handle(QueryPromotions request, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(request.User))
            {
                return await this.Reader.GetPromotionsByVendor(request.User);
            }
            return await this.Reader.GetPromotionsByVendorUnit(request.Id);
        }
    }
}

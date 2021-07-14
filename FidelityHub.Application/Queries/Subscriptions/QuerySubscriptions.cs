using FidelityHub.Application.Interfaces;
using FidelityHub.Database.Entities.UsrSchema;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FidelityHub.Application.Queries
{
    public class QuerySubscriptions : IRequest<List<Subscription>>
    {
    }

    public class QuerySubscriptionsHandler : IRequestHandler<QuerySubscriptions, List<Subscription>>
    {
        public IUserSchemaDataReader Reader { get; }

        public QuerySubscriptionsHandler(IUserSchemaDataReader reader)
        {
            this.Reader = reader;
        }

        public async Task<List<Subscription>> Handle(QuerySubscriptions request, CancellationToken cancellationToken)
        {
            return await this.Reader.GetSubscriptions();
        }
    }
}

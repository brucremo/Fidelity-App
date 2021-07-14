using FidelityHub.Application.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FidelityHub.Application.Queries.Account
{
    public class QueryResetTokenValid : IRequest<bool>
    {
        public string Token { get; }

        public QueryResetTokenValid(string token)
        {
            this.Token = token;
        }
    }

    public class ResetTokenValidRequestHadler : IRequestHandler<QueryResetTokenValid, bool>
    {
        public IDboSchemaDataReader Reader { get; }

        public ResetTokenValidRequestHadler(IDboSchemaDataReader reader)
        {
            this.Reader = reader;
        }

        public async Task<bool> Handle(QueryResetTokenValid request, CancellationToken cancellationToken)
        {
            return await this.Reader.IsResetTokenValid(request.Token);
        }
    }
}

using FidelityHub.Application.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FidelityHub.Application.Queries
{
    public class QueryUserNameExists : IRequest<bool>
    {
        public string UserName { get; }

        public QueryUserNameExists(string userName)
        {
            this.UserName = userName;
        }
    }

    public class QueryUserNameExistsHandler : IRequestHandler<QueryUserNameExists, bool>
    {
        public IDboSchemaDataReader Reader { get; }

        public QueryUserNameExistsHandler(IDboSchemaDataReader reader)
        {
            this.Reader = reader;
        }

        public async Task<bool> Handle(QueryUserNameExists request, CancellationToken cancellationToken)
        {
            return await this.Reader.UserNameExists(request.UserName);
        }
    }

}

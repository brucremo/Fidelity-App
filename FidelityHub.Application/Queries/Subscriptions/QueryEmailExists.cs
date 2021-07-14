using FidelityHub.Application.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FidelityHub.Application.Queries
{
    public class QueryEmailExists : IRequest<bool>
    {
        public string Email { get; }

        public QueryEmailExists(string email)
        {
            this.Email = email;
        }
    }

    public class QueryEmailExistsHandler : IRequestHandler<QueryEmailExists, bool>
    {
        public IDboSchemaDataReader Reader { get; }

        public QueryEmailExistsHandler(IDboSchemaDataReader reader)
        {
            this.Reader = reader;
        }

        public async Task<bool> Handle(QueryEmailExists request, CancellationToken cancellationToken)
        {
            return await this.Reader.EmailExists(request.Email);
        }
    }

}

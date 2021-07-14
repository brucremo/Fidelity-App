using FidelityHub.Application.Interfaces;
using FidelityHub.Application.Models.Registration;
using FidelityHub.Database.Entities.UsrSchema;
using MediatR;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace FidelityHub.Application.Commands.Registration
{
    public class RegisterThirdPartyRequest : IRequest<AspNetUser>
    {
        public ThirdPartyRegistrationModel Model { get; }

        public RegisterThirdPartyRequest(ThirdPartyRegistrationModel model)
        {
            this.Model = model;
        }
    }

    public class RegisterThirdPartyRequestHandler : IRequestHandler<RegisterThirdPartyRequest, AspNetUser>
    {
        private IUserSchemaDataReader Reader { get; }
        private IDboSchemaDataReader DboReader { get; }

        public RegisterThirdPartyRequestHandler(IUserSchemaDataReader reader, IDboSchemaDataReader dboReader)
        {
            this.Reader = reader;
            this.DboReader = dboReader;
        }

        public async Task<AspNetUser> Handle(RegisterThirdPartyRequest request, CancellationToken cancellationToken)
        {
            var newUser = await this.DboReader.CreateThirdPartyUser(request.Model);
            return await this.Reader.RegisterThirdPartyUser(request.Model, newUser);
        }
    }
}

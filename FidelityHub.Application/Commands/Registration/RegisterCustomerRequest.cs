using FidelityHub.Application.Interfaces;
using FidelityHub.Application.Models.Registration;
using MediatR;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace FidelityHub.Application.Commands.Registration
{
    public class RegisterCustomerRequest : IRequest<HttpStatusCode>
    {
        public CustomerRegistrationModel CustomerRegistrationModel { get; }

        public RegisterCustomerRequest(CustomerRegistrationModel customerRegistrationModel)
        {
            this.CustomerRegistrationModel = customerRegistrationModel;
        }
    }

    public class RegisterUserRequestHandler : IRequestHandler<RegisterCustomerRequest, HttpStatusCode>
    {
        private IUserSchemaDataReader Reader { get; }
        private IDboSchemaDataReader DboReader { get; }

        public RegisterUserRequestHandler(IUserSchemaDataReader reader, IDboSchemaDataReader dboReader)
        {
            this.Reader = reader;
            this.DboReader = dboReader;
        }

        public async Task<HttpStatusCode> Handle(RegisterCustomerRequest request, CancellationToken cancellationToken)
        {
            var newUser = await this.DboReader.CreateNewUser(request.CustomerRegistrationModel, await this.Reader.GetUserTypeId("Customer"));
            return await this.Reader.RegisterCustomer(request.CustomerRegistrationModel, newUser);
        }
    }
}

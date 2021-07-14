using FidelityHub.Application.Interfaces;
using FidelityHub.Application.Models.Registration;
using MediatR;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace FidelityHub.Application.Commands.Registration
{
    public class RegisterVendorRequest : IRequest<HttpStatusCode>
    {
        public VendorRegistrationModel VendorRegistrationModel { get; }

        public RegisterVendorRequest(VendorRegistrationModel vendorRegistrationModel)
        {
            this.VendorRegistrationModel = vendorRegistrationModel;
        }
    }

    public class RegisterVendorRequestHandler : IRequestHandler<RegisterVendorRequest, HttpStatusCode>
    {
        private IUserSchemaDataReader Reader { get; }
        private IDboSchemaDataReader DboReader { get; }

        public RegisterVendorRequestHandler(IUserSchemaDataReader reader, IDboSchemaDataReader dboReader)
        {
            this.Reader = reader;
            this.DboReader = dboReader;
        }

        public async Task<HttpStatusCode> Handle(RegisterVendorRequest request, CancellationToken cancellationToken)
        {
            var newUser = await this.DboReader.CreateNewUser(request.VendorRegistrationModel, await this.Reader.GetUserTypeId("VendorAdmin"));
            return await this.Reader.RegisterVendorAdmin(request.VendorRegistrationModel, newUser);
        }
    }
}

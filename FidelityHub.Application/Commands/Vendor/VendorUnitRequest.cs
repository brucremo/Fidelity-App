using FidelityHub.Application.Interfaces;
using FidelityHub.Application.Models.Vendor;
using MediatR;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace FidelityHub.Application.Commands.Vendor
{
    public class VendorUnitRequest
    {
        // --- Commands ---
        public class Create : IRequest<HttpStatusCode>
        {
            public VendorUnitViewModel VendorUnit { get; }

            public Create(VendorUnitViewModel VendorUnit)
            {
                this.VendorUnit = VendorUnit;
            }
        }

        public class Update : IRequest<HttpStatusCode>
        {
            public VendorUnitViewModel VendorUnit { get; }

            public Update(VendorUnitViewModel VendorUnit)
            {
                this.VendorUnit = VendorUnit;
            }
        }

        // --- Handlers ---
        public class CreateVendorUnitRequestHandler : IRequestHandler<Create, HttpStatusCode>
        {
            public IAppSchemaDataReader Reader { get; }

            public CreateVendorUnitRequestHandler(IAppSchemaDataReader reader)
            {
                this.Reader = reader;
            }

            public async Task<HttpStatusCode> Handle(Create request, CancellationToken cancellationToken)
            {
                await this.Reader.CreateVendorUnit(request.VendorUnit);
                return HttpStatusCode.OK;
            }
        }

        public class UpdateVendorUnitRequestHandler : IRequestHandler<Update, HttpStatusCode>
        {
            public IAppSchemaDataReader Reader { get; }

            public UpdateVendorUnitRequestHandler(IAppSchemaDataReader reader)
            {
                this.Reader = reader;
            }

            public async Task<HttpStatusCode> Handle(Update request, CancellationToken cancellationToken)
            {
                await this.Reader.UpdateVendorUnit(request.VendorUnit);
                return HttpStatusCode.OK;
            }
        }
    }
}

using FidelityHub.Application.Interfaces;
using FidelityHub.Application.Models.Promotion;
using MediatR;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace FidelityHub.Application.Commands.Vendor
{
    public class PromotionRequest
    {
        // --- Commands ---
        public class Create : IRequest<HttpStatusCode>
        {
            public PromotionViewModel Promotion { get; }

            public Create(PromotionViewModel promotion)
            {
                this.Promotion = promotion;
            }
        }

        public class Update : IRequest<HttpStatusCode>
        {
            public PromotionViewModel Promotion { get; }

            public Update(PromotionViewModel promotion)
            {
                this.Promotion = promotion;
            }
        }

        // --- Handlers ---
        public class CreatePromotionRequestHandler : IRequestHandler<Create, HttpStatusCode>
        {
            public IAppSchemaDataReader Reader { get; }

            public CreatePromotionRequestHandler(IAppSchemaDataReader reader)
            {
                this.Reader = reader;
            }

            public async Task<HttpStatusCode> Handle(Create request, CancellationToken cancellationToken)
            {
                await this.Reader.CreatePromotion(request.Promotion);
                return HttpStatusCode.OK;
            }
        }

        public class UpdatePromotionRequestHandler : IRequestHandler<Update, HttpStatusCode>
        {
            public IAppSchemaDataReader Reader { get; }

            public UpdatePromotionRequestHandler(IAppSchemaDataReader reader)
            {
                this.Reader = reader;
            }

            public async Task<HttpStatusCode> Handle(Update request, CancellationToken cancellationToken)
            {
                await this.Reader.UpdatePromotion(request.Promotion);
                return HttpStatusCode.OK;
            }
        }
    }
}

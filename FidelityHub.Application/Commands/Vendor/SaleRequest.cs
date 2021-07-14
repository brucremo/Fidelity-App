using FidelityHub.Application.Interfaces;
using FidelityHub.Application.Models.Shared;
using FidelityHub.Application.Models.Vendor;
using FidelityHub.Database.Entities.AppSchema;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FidelityHub.Application.Commands.Vendor
{
    public class SaleRequest
    {
        #region Requests
        public class Create : IRequest<SaleResultModel>
        {
            public SaleRequestModel SaleRequestModel { get; }

            public Create(SaleRequestModel request)
            {
                this.SaleRequestModel = request;
            }
        }

        public class Update : IRequest<IEnumerable<PromotionSale>>
        {
            public IEnumerable<PromotionSale> Sales { get; }

            public Update(IEnumerable<PromotionSale> sales)
            {
                this.Sales = sales;
            }
        }

        public class Delete : IRequest<IEnumerable<PromotionSale>>
        {
            public IEnumerable<PromotionSale> Sales { get; }

            public Delete(IEnumerable<PromotionSale> sales)
            {
                this.Sales = sales;
            }
        }
        #endregion

        #region Handlers
        public class CreateSalesRequestHandler : IRequestHandler<Create, SaleResultModel>
        {
            public IAppSchemaDataReader Reader { get; }

            public CreateSalesRequestHandler(IAppSchemaDataReader reader)
            {
                this.Reader = reader;
            }

            public async Task<SaleResultModel> Handle(Create request, CancellationToken cancellationToken)
            {
                if (!await this.Reader.IsCustomerRegisteredForPromotion(request.SaleRequestModel.UserId, request.SaleRequestModel.PromotionId))
                {
                    await this.Reader.RegisterCustomerForPromotion(request.SaleRequestModel.UserId, request.SaleRequestModel.PromotionId);
                }

                return await this.Reader.RegisterVendorSale(request.SaleRequestModel);
            }
        }

        public class UpdateSalesRequestHandler : IRequestHandler<Update, IEnumerable<PromotionSale>>
        {
            public IAppSchemaDataReader Reader { get; }

            public UpdateSalesRequestHandler(IAppSchemaDataReader reader)
            {
                this.Reader = reader;
            }

            public async Task<IEnumerable<PromotionSale>> Handle(Update request, CancellationToken cancellationToken)
            {
                return await this.Reader.UpdateSales(request.Sales);
            }
        }

        public class DeleteSalesRequestHandler : IRequestHandler<Delete, IEnumerable<PromotionSale>>
        {
            public IAppSchemaDataReader Reader { get; }

            public DeleteSalesRequestHandler(IAppSchemaDataReader reader)
            {
                this.Reader = reader;
            }

            public async Task<IEnumerable<PromotionSale>> Handle(Delete request, CancellationToken cancellationToken)
            {
                return await this.Reader.DeleteSales(request.Sales);
            }
        }
        #endregion
    }
}

using FidelityHub.Application.Interfaces;
using FidelityHub.Application.Models.Vendor;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FidelityHub.Application.Commands.Vendor
{
    public class RegisterRewardRequest : IRequest<bool>
    {
        public SaleRequestModel SaleRequestModel { get; }

        public RegisterRewardRequest(SaleRequestModel request)
        {
            this.SaleRequestModel = request;
        }
    }

    public class RegisterRewardRequestHandler : IRequestHandler<RegisterRewardRequest, bool>
    {
        public IAppSchemaDataReader Reader { get; }

        public RegisterRewardRequestHandler(IAppSchemaDataReader reader)
        {
            this.Reader = reader;
        }

        public async Task<bool> Handle(RegisterRewardRequest request, CancellationToken cancellationToken)
        {
            return await this.Reader.RegisterReward(request.SaleRequestModel.UserId, request.SaleRequestModel.PromotionId);
        }
    }
}

using FidelityHub.Application.Interfaces;
using FidelityHub.Application.Models.Shared;

namespace FidelityHub.Application.Models.Vendor
{
    public class SaleResultModel
    {
        public string UserId { get; set; }
        public float AmountUntilReward { get; set; }
        public string VendorName { get; set; }
        public string PromotionName { get; set; }
    }
}

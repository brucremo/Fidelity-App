namespace FidelityHub.Application.Models.Vendor
{
    public class SaleRequestModel
    {
        public int PromotionId { get; set; }
        public string UserId { get; set; }
        public int Amount { get; set; }
        public bool Approved { get; set; }
    }
}

using FidelityHub.Database.Entities.UsrSchema;
using System;
using System.Collections.Generic;
using System.Text;

namespace FidelityHub.Database.Entities.AppSchema
{
    public class SalesByVendorUnit
    {
        public int VendorUnitId { get; set; }
        public VendorUnit VendorUnit { get; set; }
        public int SaleId { get; set; }
        public PromotionSale PromotionSale { get; set; }
        public int PromotionId { get; set; }
        public Promotion Promotion { get; set; }
        public string UserId { get; set; }
        public AspNetUser User { get; set; }
        public DateTime Timestamp { get; set; }
        public int Amount { get; set; }
    }
}

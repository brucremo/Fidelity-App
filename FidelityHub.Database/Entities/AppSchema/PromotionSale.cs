using FidelityHub.Database.Entities.UsrSchema;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FidelityHub.Database.Entities.AppSchema
{
    public class PromotionSale
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int PromotionId { get; set; }
        public Promotion Promotion { get; set; }
        public string UserId { get; set; }
        [NotMapped]
        public string UserName { get; set; }
        public DateTime Timestamp { get; set; }
        public decimal Amount { get; set; }
        public bool? Approved { get; set; }
        [ForeignKey("SellerId_UserId")]
        public string SellerId { get; set; }
        [NotMapped]
        public string SellerName { get; set; }
        public bool? Removed { get; set; }
    }
}

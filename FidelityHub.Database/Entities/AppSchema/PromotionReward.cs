using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FidelityHub.Database.Entities.AppSchema
{
    public class PromotionReward
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int PromotionId { get; set; }
        public string CustomerId { get; set; }
        public DateTime Timestamp { get; set; }
        public bool? CustomerReviewed { get; set; }
        public DateTime? CustomerReviewTimestamp { get; set; }
        public bool? VendorReviewed { get; set; }
        public DateTime? VendorReviewTimestamp { get; set; }
    }
}

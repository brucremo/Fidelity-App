using System.ComponentModel.DataAnnotations.Schema;

namespace FidelityHub.Database.Entities.AppSchema
{
    public partial class PromotionCustomer
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string CustomerId { get; set; }
        public int PromotionId { get; set; }
    }
}

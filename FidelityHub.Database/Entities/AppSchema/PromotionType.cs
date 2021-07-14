using System.ComponentModel.DataAnnotations.Schema;

namespace FidelityHub.Database.Entities.AppSchema
{
    public class PromotionType
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Description { get; set; }
        public float Threshold { get; set; }
    }
}

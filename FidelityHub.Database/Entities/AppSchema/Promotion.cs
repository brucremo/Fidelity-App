using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FidelityHub.Database.Entities.AppSchema
{
    public class Promotion
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int PromotionHourStart { get; set; }
        public int PromotionHourEnd { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        //FKs
        public int VendorUnitId { get; set; }
        public VendorUnit VendorUnit { get; set; }
        public int PromotionTypeId { get; set; }
        public PromotionType PromotionType { get; set; }
    }
}

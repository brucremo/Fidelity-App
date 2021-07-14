using FidelityHub.Application.Models.Vendor;
using FidelityHub.Database.Entities.AppSchema;
using System;
using System.Collections.Generic;

namespace FidelityHub.Application.Models.Promotion
{
    public class PromotionViewModel
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<DateTime> StartHours { get; set; }
        public List<DateTime> EndHours { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public PromotionType PromotionType { get; set; }
        public VendorUnit VendorUnit { get; set; }

        public PromotionViewModel()
        {

        }

        public PromotionViewModel(Database.Entities.AppSchema.Promotion promotion, PromotionType type, VendorUnit vendorUnit)
        {
            this.PromotionType = type;
            this.VendorUnit = vendorUnit;

            this.Id = promotion.Id;
            this.StartDate = promotion.StartDate;
            this.EndDate = promotion.EndDate;
            this.StartHours = null; //promotion.PromotionHourStart;
            this.EndHours = null; //promotion.PromotionHourStart;
            this.Description = promotion.Description;
            this.Active = promotion.Active;
        }

        public Database.Entities.AppSchema.Promotion GetPromotionDBModel()
        {
            return new Database.Entities.AppSchema.Promotion
            {
                Id = this.Id,
                VendorUnitId = this.VendorUnit.Id,
                StartDate = this.StartDate,
                EndDate = this.EndDate,
                Description = this.Description,
                Active = this.Active,
                PromotionTypeId = this.PromotionType.Id
            };
        }
    }
}

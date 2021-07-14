using FidelityHub.Database.Entities.AppSchema;
using System;
using System.Collections.Generic;
using System.Text;

namespace FidelityHub.Application.Models.Sale
{
    public class SalesViewModel
    {
        public VendorUnit VendorUnit { get; set; }
        public IEnumerable<PromotionSale> Sales { get; set; }

        public SalesViewModel()
        {
        }
    }

    public class SalesModel
    {
        public Database.Entities.AppSchema.Promotion Promotion { get; set; }
        public List<PromotionSale> Sales { get; set; }
    }
}

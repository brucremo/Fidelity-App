using FidelityHub.Application.Models.Promotion;
using FidelityHub.Database.Entities.AppSchema;
using System.Collections.Generic;

namespace FidelityHub.Application.Models.Customer
{
    public class CustomerPromotionDashboardViewModel
    {
        public PromotionViewModel Promotion { get; set; }
        public IEnumerable<PromotionSale> Purchases { get; set; }
    }
}

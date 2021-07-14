using FidelityHub.Database.Entities.AppSchema;
using System.Collections.Generic;

namespace FidelityHub.Application.Models.Sale
{
    public class SalesChartModel
    {
        public string CategoryName { get; set; }
        public IEnumerable<PromotionSale> Sales { get; set; }
    }
}

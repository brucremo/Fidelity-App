using System;
using System.Collections.Generic;

namespace FidelityHub.Application.Models.Vendor.Dashboard
{
    public class VendorSaleViewModel
    {
        public DateTime PeriodStart { get; set; }
        public DateTime PeriodEnd { get; set; }
        public List<VendorSaleModel> Sales { get; set; }
    }
}

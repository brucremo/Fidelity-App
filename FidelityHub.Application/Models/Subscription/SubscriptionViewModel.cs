using System;
using System.Collections.Generic;
using System.Text;

namespace FidelityHub.Application.Models.Subscription
{
    public class SubscriptionViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int RecurrenceDays { get; set; }
        public float Price { get; set; }
        public string Title { get; set; }
        public bool? Active { get; set; }
    }
}

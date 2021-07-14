using System.Data.SqlTypes;

namespace FidelityHub.Database.Entities.UsrSchema
{
    public partial class Subscription
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int RecurrenceDays { get; set; }
        public decimal? Price { get; set; }
        public bool? Active { get; set; }
    }
}

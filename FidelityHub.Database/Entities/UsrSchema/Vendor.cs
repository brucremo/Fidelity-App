using FidelityHub.Database.Entities.UsrSchema;
using System.ComponentModel.DataAnnotations.Schema;

namespace FidelityHub.Database.Entities.UsrSchema
{
    public partial class Vendor
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string LegalName { get; set; }
        public string GovernmentId { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Description { get; set; }
        public int LOBId { get; set; }
        //FKs
        public int UserTypeId { get; set; }
        public UserType UserType { get; set; }
        public int SubscriptionId { get; set; }
        public Subscription Subscription { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
        // View Support
        public string UserId { get; set; }
    }
}
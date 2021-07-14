using FidelityHub.Application.Models.Shared;
using FidelityHub.Database.Entities.UsrSchema;

namespace FidelityHub.Application.Models.Registration
{
    public partial class VendorRegistrationModel : BaseRegistrationModel
    {
        // usr.Vendor
        public string GovernmentId { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Description { get; set; }
        public int LOBId { get; set; }
        public int SubscriptionId { get; set; }
        

        public Database.Entities.UsrSchema.Vendor GetVendorEntity(int addressId, int userTypeId, string userId)
        {
            return new Database.Entities.UsrSchema.Vendor
            {
                UserId = userId,
                UserTypeId = userTypeId,
                UserName = this.UserName,
                LegalName = this.LegalName,
                GovernmentId = this.GovernmentId,
                Email = this.Email,
                Phone = this.Phone,
                Mobile = this.Mobile,
                Description = this.Description,
                LOBId = this.LOBId,
                SubscriptionId = this.SubscriptionId,
                AddressId = addressId
            };
        }
    }
}
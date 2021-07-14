using FidelityHub.Database.Entities.UsrSchema;

namespace FidelityHub.Application.Models.Vendor
{
    public class VendorViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string LegalName { get; set; }
        public string GovernmentId { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
        public int LOB { get; set; }
        public string Mobile { get; set; }
        public Address Address { get; set; }

        public VendorViewModel() { }

        public VendorViewModel(Database.Entities.UsrSchema.Vendor vendor)
        {
            this.Id = vendor.Id;
            this.UserName = vendor.UserName;
            this.LegalName = vendor.LegalName;
            this.GovernmentId = vendor.GovernmentId;
            this.Email = vendor.Email;
            this.Phone = vendor.Phone;
            this.Description = vendor.Description;
            this.LOB = vendor.LOBId;
            this.Mobile = vendor.Mobile;
            this.Address = vendor.Address;
        }
    }
}

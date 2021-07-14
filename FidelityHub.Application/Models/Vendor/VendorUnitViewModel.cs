using FidelityHub.Database.Entities.AppSchema;
using FidelityHub.Database.Entities.UsrSchema;
using System;
using System.Collections.Generic;
using System.Text;

namespace FidelityHub.Application.Models.Vendor
{
    public class VendorUnitViewModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Description { get; set; }
        public string Website { get; set; }
        public int OpeningHours { get; set; }
        public int ClosingHours { get; set; }
        public VendorViewModel Vendor { get; set; }

        public VendorUnit GetVendorUnitDBModel()
        {
            return new VendorUnit
            {
                Id = this.Id,
                Email = this.Email,
                AddressId = this.Address.Id,
                Phone = this.Phone,
                Mobile = this.Mobile,
                Description = this.Description,
                Website = this.Website,
                OpeningHours = this.OpeningHours,
                ClosingHours = this.ClosingHours,
                VendorId = this.Vendor.Id
            };
        }
    }
}

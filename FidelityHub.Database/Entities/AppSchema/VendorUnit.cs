using FidelityHub.Database.Entities.UsrSchema;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FidelityHub.Database.Entities.AppSchema
{
    public class VendorUnit
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Description { get; set; }
        public string Website { get; set; }
        public int OpeningHours { get; set; }
        public int ClosingHours { get; set; }
        //FKs
        public int VendorId { get; set; }
        public Vendor Vendor { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
    }
}

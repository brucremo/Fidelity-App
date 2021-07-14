using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FidelityHub.Database.Entities.UsrSchema
{
    public class Customer
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string LegalName { get; set; }
        public string GovernmentId { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        //FKs
        public int? UserTypeId { get; set; }
        public UserType UserType { get; set; }
        public int? UserGenderId { get; set; }
        public UserGender UserGender { get; set; }
        public int? AddressId { get; set; }
        public Address Address { get; set; }
    }
}

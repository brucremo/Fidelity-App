using FidelityHub.Database.Entities.UsrSchema;
using System;
using System.Collections.Generic;
using System.Text;

namespace FidelityHub.Application.Models.Shared
{
    public class BaseRegistrationModel
    {
        public int UserTypeId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string LegalName { get; set; }
        // usr.Address
        public bool HasAddress { get; set; }
        public string PostalCode { get; set; }
        public int? StreetNumber { get; set; }
        public string Street { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Complement { get; set; }

        public Address GetAddressEntity()
        {
            if (this.HasAddress)
            {
                return new Address
                {
                    PostalCode = this.PostalCode,
                    StreetNumber = this.StreetNumber.Value,
                    Street = this.Street,
                    Region = this.Region,
                    City = this.City,
                    State = this.State,
                    Country = this.Country,
                    Complement = this.Complement
                };
            }
            else
            {
                return null;
            }
        }
    }
}

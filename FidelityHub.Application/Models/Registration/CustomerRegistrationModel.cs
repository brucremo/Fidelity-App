using FidelityHub.Application.Models.Shared;
using FidelityHub.Database.Entities.UsrSchema;
using System;
using System.Collections.Generic;
using System.Text;

namespace FidelityHub.Application.Models.Registration
{
    public class CustomerRegistrationModel : BaseRegistrationModel
    {
        // usr.Customer
        public string GovernmentId { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public int? UserGenderId { get; set; }

        public Database.Entities.UsrSchema.Customer GetCustomerEntity(string id, int? addressId, int userTypeId)
        {
            return new Database.Entities.UsrSchema.Customer
            {
                Id = id,
                UserTypeId = userTypeId,
                UserName = this.UserName,
                LegalName = this.LegalName,
                GovernmentId = this.GovernmentId,
                Email = this.Email,
                Phone = this.Phone,
                Mobile = this.Mobile,
                UserGenderId = UserGenderId,
                AddressId = addressId
            };
        }
    }
}

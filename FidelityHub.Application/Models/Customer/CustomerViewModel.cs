using FidelityHub.Database.Entities.UsrSchema;
using System;
using System.Collections.Generic;
using System.Text;

namespace FidelityHub.Application.Models.Customer
{
    public class CustomerViewModel
    {
        public string Id { get; set; }
        public string LegalName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string UserName { get; set; }

        public CustomerViewModel(Database.Entities.UsrSchema.Customer customer)
        {
            this.Id = customer.Id;
            this.LegalName = customer.LegalName;
            this.Email = customer.Email;
            this.Phone = customer.Email;
            this.Mobile = customer.Mobile;
            this.UserName = customer.UserName;
        }
    }
}

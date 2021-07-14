using FidelityHub.Application.Models.Registration;
using FidelityHub.Database.Entities.UsrSchema;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace FidelityHub.Application.Interfaces
{
    public interface IUserSchemaDataReader
    {
        // --- Commands ---
        public Task<HttpStatusCode> RegisterVendorAdmin(VendorRegistrationModel vendorModel, AspNetUser newUser);
        public Task<HttpStatusCode> RegisterCustomer(CustomerRegistrationModel customerModel, AspNetUser newUser);
        public Task<AspNetUser> RegisterThirdPartyUser(ThirdPartyRegistrationModel registrationModel, AspNetUser newUser);

        // --- Queries ---
        public Task<List<Subscription>> GetSubscriptions();
        public Task<int> GetUserTypeId(string userTypeName);
    }
}

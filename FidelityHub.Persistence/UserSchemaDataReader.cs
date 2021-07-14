using AutoMapper;
using FidelityHub.Application.Interfaces;
using FidelityHub.Application.Models.Registration;
using FidelityHub.Database.Entities.UsrSchema;
using FidelityHub.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace FidelityHub.Infrastructure.Readers
{
    public class UserSchemaDataReader : IUserSchemaDataReader
    {
        private UserDbContext Context { get; }

        public UserSchemaDataReader(UserDbContext context)
        {
            this.Context = context;
        }

        // --- Commands ---
        public async Task<HttpStatusCode> RegisterVendorAdmin(VendorRegistrationModel vendorModel, AspNetUser newUser)
        {
            var address = await this.Context.Addresses.AddAsync(vendorModel.GetAddressEntity());
            await this.Context.SaveChangesAsync();

            var userType = await this.Context.UserTypes.FirstOrDefaultAsync(x => x.TypeDescription.Trim() == "VendorAdmin");

            await this.Context.Vendors.AddAsync(vendorModel.GetVendorEntity(address.Entity.Id, userType.UserTypeId, newUser.Id));

            await this.Context.SaveChangesAsync();
            return HttpStatusCode.OK;
        }

        public async Task<HttpStatusCode> RegisterCustomer(CustomerRegistrationModel customerModel, AspNetUser newUser)
        {
            if (customerModel.HasAddress)
            {
                var address = await this.Context.Addresses.AddAsync(customerModel.GetAddressEntity());
                await this.Context.SaveChangesAsync();

                var userType = await this.Context.UserTypes.FirstOrDefaultAsync(x => x.TypeDescription.Trim() == "Customer");

                await this.Context.Customers.AddAsync(customerModel.GetCustomerEntity(newUser.Id, address.Entity.Id, userType.UserTypeId));
            }
            else
            {
                var userType = await this.Context.UserTypes.FirstOrDefaultAsync(x => x.TypeDescription.Trim() == "Customer");

                await this.Context.Customers.AddAsync(customerModel.GetCustomerEntity(newUser.Id, null, userType.UserTypeId));
            }

            await this.Context.SaveChangesAsync();
            return HttpStatusCode.OK;
        }

        public async Task<AspNetUser> RegisterThirdPartyUser(ThirdPartyRegistrationModel registrationModel, AspNetUser newUser)
        {
            var userType = await this.Context.UserTypes.FirstOrDefaultAsync(x => x.TypeDescription.Trim() == "Customer");

            var customer = registrationModel.GetCustomerEntity(newUser.Id, userType.UserTypeId);

            await this.Context.Customers.AddAsync(customer);

            await this.Context.SaveChangesAsync();

            return newUser;
        }

        // --- Queries ---
        public async Task<List<Subscription>> GetSubscriptions()
        {
            return await this.Context.Subscriptions.ToListAsync();
        }

        public async Task<int> GetUserTypeId(string userTypeName)
        {
            var type = await this.Context.UserTypes.FirstOrDefaultAsync(x => x.TypeDescription.Trim().ToLower() == userTypeName.Trim().ToLower());
            return type.UserTypeId;
        }
    }
}

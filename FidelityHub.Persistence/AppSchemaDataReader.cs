using FidelityHub.Application.Interfaces;
using FidelityHub.Application.Models.Customer;
using FidelityHub.Application.Models.Promotion;
using FidelityHub.Application.Models.Vendor;
using FidelityHub.Application.Models.Shared;
using FidelityHub.Database.Entities.AppSchema;
using FidelityHub.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FidelityHub.Application.Models.Vendor.Dashboard;
using FidelityHub.Database.Entities.UsrSchema;
using FidelityHub.Application.Extensions;
using FidelityHub.Application.Behaviors.Exceptions;
using FidelityHub.Application.Models.Sale;

namespace FidelityHub.Persistence
{
    public class AppSchemaDataReader : IAppSchemaDataReader
    {
        private AppDbContext Context { get; }
        private UserDbContext UserContext { get; }
        private DboDbContext DboContext { get; }

        public AppSchemaDataReader(AppDbContext context, DboDbContext dboContext, UserDbContext userContext)
        {
            this.Context = context;
            this.UserContext = userContext;
            this.DboContext = dboContext;
        }

        #region Reward
        public async Task<bool> ReviewReward(string userId, int rewardId)
        {
            var reward = await this.Context.PromotionRewards.FirstOrDefaultAsync(x => x.Id == rewardId);
            var user = await this.DboContext.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user.UserTypeId == 3 || user.UserTypeId == 5)
            {
                reward.VendorReviewed = true;
                reward.VendorReviewTimestamp = DateTime.UtcNow;
            }
            else if (user.UserTypeId == 4)
            {
                reward.CustomerReviewed = true;
                reward.CustomerReviewTimestamp = DateTime.UtcNow;
            }
            else
            {
                throw new InvalidOperationException("ERRO: Somente Clientes ou Lojistas podem confirmar a recompensa");
            }

            this.Context.PromotionRewards.Update(reward);

            return await this.Context.SaveChangesAsync() > 0;
        }
        #endregion

        #region VendorUnit
        public async Task<List<VendorUnitViewModel>> GetVendorUnits(string user)
        {
            var vendor = await this.GetVendorByUserName(user);

            var units = await this.Context.VendorUnits
                .Where(x => x.VendorId == vendor.Id).ToListAsync();

            List<VendorUnitViewModel> viewModels = new List<VendorUnitViewModel>();

            foreach(var x in units)
            {
                viewModels.Add(new VendorUnitViewModel
                {
                    Id = x.Id,
                    Email = x.Email,
                    Address = await this.UserContext.Addresses.FirstOrDefaultAsync(y => y.Id == x.AddressId),
                    Phone = x.Phone,
                    Mobile = x.Mobile,
                    Description = x.Description,
                    Website = x.Website,
                    OpeningHours = x.OpeningHours,
                    ClosingHours = x.ClosingHours,
                    Vendor = null
                });
            }

            return viewModels;
        } 

        public async Task<bool> CreateVendorUnit(VendorUnitViewModel viewModel)
        {
            if (await this.UserContext.Addresses.FirstOrDefaultAsync(x => x.Id == viewModel.Address.Id) == null)
            {
                await this.Context.AddAsync(viewModel.Address);
            }
            
            await this.Context.AddAsync(viewModel.GetVendorUnitDBModel());
            return await this.Context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateVendorUnit(VendorUnitViewModel viewModel)
        {
            this.Context.Update(viewModel.GetVendorUnitDBModel());
            return await this.Context.SaveChangesAsync() > 0;
        }
        #endregion

        #region Vendor
        private async Task<Vendor> GetVendorByUserName(string userName)
        {
            var dbUser = await this.DboContext.Users.FirstOrDefaultAsync(x => x.UserName.Trim() == userName.Trim());
            var vendorUser = await this.UserContext.VendorRelationship.FirstOrDefaultAsync(x => x.UserId == dbUser.Id);
            return await this.UserContext.Vendors.FirstOrDefaultAsync(x => x.Id == vendorUser.VendorId);
        }

        private PromotionSale GetPromotionSaleDBModel(SaleRequestModel request, string userId)
        {
            return new PromotionSale
            {
                PromotionId = request.PromotionId,
                UserId = userId,
                Timestamp = DateTime.UtcNow,
                Amount = request.Amount
            };
        }

        public async Task<SaleResultModel> RegisterVendorSale(SaleRequestModel request)
        {
            var appUser = await this.DboContext.Users.FirstOrDefaultAsync(x => x.Id == request.UserId || x.UserName == request.UserId);
            await this.Context.PromotionSales.AddAsync(this.GetPromotionSaleDBModel(request, appUser.Id));
            await this.Context.SaveChangesAsync();

            var promotion = await this.Context.Promotions.FirstOrDefaultAsync(x => x.Id == request.PromotionId);
            var promoType = await this.Context.PromotionTypes.FirstOrDefaultAsync(x => x.Id == promotion.PromotionTypeId);
            var vendor = await this.Context.VendorUnits.FirstOrDefaultAsync(x => x.Id == promotion.VendorUnitId);

            return new SaleResultModel
            {
                UserId = appUser.Id,
                AmountUntilReward = promoType.Threshold - await this.Context.PromotionSales.Where(x => x.PromotionId == promotion.Id).CountAsync(),
                PromotionName = promotion.Description,
                VendorName = vendor.Description
            };
        }

        public async Task<IEnumerable<PromotionSale>> UpdateSales(IEnumerable<PromotionSale> models)
        {
            this.Context.PromotionSales.UpdateRange(models);
            await this.Context.SaveChangesAsync();
            return models;
        }

        public async Task<IEnumerable<PromotionSale>> DeleteSales(IEnumerable<PromotionSale> models)
        {
            this.Context.PromotionSales.RemoveRange(models);

            if (this.Context.IsChangeSuccessful(models.Count()))
            {
                await this.Context.SaveChangesAsync();
                return models;
            }
            else
            {
                this.Context.Rollback();
                throw new FailedRequestException("Unable to delete entities");
            }
        }

        public async Task<bool> IsCustomerRegisteredForPromotion(string customerId, int promotionId)
        {
            var customer = await this.DboContext.Users.FirstOrDefaultAsync(x => x.UserName == customerId || x.Id == customerId);
            return await this.Context.PromotionCustomers.CountAsync(x => x.CustomerId == customer.Id && x.PromotionId == promotionId) > 0;
        }

        public async Task<bool> RegisterCustomerForPromotion(string customerId, int promotionId)
        {
            var customer = await this.DboContext.Users.FirstOrDefaultAsync(x => x.UserName == customerId || x.Id == customerId);
            await this.Context.PromotionCustomers.AddAsync(new PromotionCustomer
            {
                CustomerId = customer.Id,
                PromotionId = promotionId
            });

            return await this.Context.SaveChangesAsync() > 0;
        }

        public async Task<bool> RegisterReward(string customerId, int promotionId)
        {
            await this.Context.PromotionRewards.AddAsync(new PromotionReward
            {
                PromotionId = promotionId,
                CustomerId = customerId,
                Timestamp = DateTime.UtcNow
            });

            return await this.Context.SaveChangesAsync() > 0;
        }
        #endregion

        #region Vendor Dashboard
        public async Task<List<VendorSaleModel>> GetVendorSalesAsync(DateTime to, DateTime from)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Promotion
        public async Task<IEnumerable<CustomerPromotionDashboardViewModel>> GetCustomerEnrolledPromotions(string customerId)
        {
            var enrolledPromotions = await this.Context.PromotionCustomers
                .Where(x => x.CustomerId == customerId)
                .Distinct()
                .ToListAsync();

            List<CustomerPromotionDashboardViewModel> models = new List<CustomerPromotionDashboardViewModel>();

            foreach (var ep in enrolledPromotions)
            {
                var promotion = await this.Context.Promotions.FirstOrDefaultAsync(x => x.Id == ep.PromotionId);
                var vendorUnit = await this.Context.VendorUnits.FirstOrDefaultAsync(x => x.Id == promotion.VendorUnitId);
                var type = await this.Context.PromotionTypes.FirstOrDefaultAsync(x => x.Id == promotion.PromotionTypeId);
                var promoModel = new PromotionViewModel(promotion, type, vendorUnit);

                models.Add(new CustomerPromotionDashboardViewModel {
                    Promotion = promoModel,
                    Purchases = await this.Context.PromotionSales.Where(x => x.PromotionId == promotion.Id && x.UserId == customerId).ToListAsync()
                });
            }

            return models;
        }

        public async Task<IEnumerable<Promotion>> GetPromotionsByVendorUnit(int vendorUnitId)
        {
            return await this.Context.Promotions.Where(x => x.VendorUnitId == vendorUnitId).ToListAsync();
        }

        public async Task<IEnumerable<Promotion>> GetPromotionsByVendor(string user)
        {
            var vendor = await this.GetVendorByUserName(user);
            var units = await this.Context.VendorUnits.Where(x => x.VendorId == vendor.Id).Select(x => x.Id).ToListAsync();
            return await this.Context.Promotions.Where(x => units.Contains(x.VendorUnitId)).ToListAsync();
        }

        public async Task<SalesViewModel> GetPromotionSaleByVendorUnit(int vendorUnitId, DateTime from, DateTime to)
        {
            var vendorUnit = await this.Context.VendorUnits.FirstOrDefaultAsync(x => x.Id == vendorUnitId);
            var promotions = await this.Context.Promotions.Where(x => x.VendorUnitId == vendorUnitId).ToListAsync();
            var sales = await this.Context.PromotionSales.Where(y => y.Timestamp >= from && y.Timestamp <= to).ToListAsync();

            List<PromotionSale> salesModels = new List<PromotionSale>();

            foreach(PromotionSale sale in sales)
            {
                var user = await this.DboContext.Users.FirstOrDefaultAsync(x => x.Id == sale.UserId);
                var seller = await this.DboContext.Users.FirstOrDefaultAsync(x => x.Id == sale.SellerId);
                sale.UserName = user.UserName;
                sale.SellerName = seller.UserName;
                salesModels.Add(sale);
            }

            return new SalesViewModel
            {
                VendorUnit = vendorUnit,
                Sales = salesModels 
            };
        }

        public async Task<bool> CreatePromotion(PromotionViewModel viewModel)
        {
            await this.Context.AddAsync(viewModel.GetPromotionDBModel());
            return await this.Context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdatePromotion(PromotionViewModel viewModel)
        {
            this.Context.Update(viewModel.GetPromotionDBModel());
            return await this.Context.SaveChangesAsync() > 0;
        }
        #endregion

        #region Customer
        public async Task<IEnumerable<VendorViewModel>> GetCustomerEnrolledVendors(string customerId)
        {
            return null;
        }

        public async Task<SalesModel> GetCustomerSalesByPromotion(string customerId, int promotionId)
        {
            return new SalesModel
            {
                Promotion = await this.Context.Promotions
                    .FirstOrDefaultAsync(x => x.Id == promotionId),
                Sales = await this.Context.PromotionSales
                    .Where(x => x.PromotionId == promotionId && x.UserId == customerId)
                    .ToListAsync()
            };
        }

        public async Task<IEnumerable<CustomerViewModel>> GetAllCustomersEnrolledByVendorUnitAndPromotion(int vendorUnitId, int promotionId)
        {
            //return await this.UserContext.Customers
            //    .FromSqlRaw("SELECT * FROM [app].[CustomersEnrolledOnPromotion]")
            //    .Where(x => x.VendorUnitId == vendorUnitId && x.PromotionId == promotionId)
            //    .Select(x => new CustomerViewModel(x))
            //    .ToListAsync();
            return null;
        }

        public async Task<IEnumerable<CustomerViewModel>> GetAllCustomersEnrolledByVendorUnit(int vendorUnitId)
        {
            //return await this.UserContext.Customers
            //    .FromSqlRaw("SELECT * FROM [app].[CustomersEnrolledOnPromotion]")
            //    .Where(x => x.VendorUnitId == vendorUnitId)
            //    .Select(x => new CustomerViewModel(x))
            //    .ToListAsync();
            return null;
        }

        public async Task<IEnumerable<CustomerViewModel>> GetAllCustomersEnrolledByPromotion(int promotionId)
        {
            //return await this.UserContext.Customers
            //    .FromSqlRaw("SELECT * FROM [app].[CustomersEnrolledOnPromotion]")
            //    .Where(x => x.PromotionId == promotionId)
            //    .Select(x => new CustomerViewModel(x))
            //    .ToListAsync();
            return null;
        }

        #endregion
    }
}

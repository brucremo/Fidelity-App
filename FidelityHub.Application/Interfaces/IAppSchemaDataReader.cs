using FidelityHub.Application.Models.Customer;
using FidelityHub.Application.Models.Promotion;
using FidelityHub.Application.Models.Vendor;
using FidelityHub.Application.Models.Shared;
using FidelityHub.Database.Entities.AppSchema;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using FidelityHub.Application.Models.Sale;

namespace FidelityHub.Application.Interfaces
{
    public interface IAppSchemaDataReader
    {
        #region Reward
        Task<bool> ReviewReward(string userId, int rewardId);
        #endregion

        #region VendorUnit
        Task<List<VendorUnitViewModel>> GetVendorUnits(string user);
        Task<bool> CreateVendorUnit(VendorUnitViewModel viewModel);
        Task<bool> UpdateVendorUnit(VendorUnitViewModel viewModel);
        #endregion

        #region Vendor
        Task<SaleResultModel> RegisterVendorSale(SaleRequestModel request);
        Task<IEnumerable<PromotionSale>> UpdateSales(IEnumerable<PromotionSale> models);
        Task<IEnumerable<PromotionSale>> DeleteSales(IEnumerable<PromotionSale> models);
        Task<IEnumerable<Promotion>> GetPromotionsByVendorUnit(int vendorUnitId);
        Task<SalesViewModel> GetPromotionSaleByVendorUnit(int vendorUnitId, DateTime from, DateTime to);
        Task<IEnumerable<Promotion>> GetPromotionsByVendor(string user);
        Task<bool> IsCustomerRegisteredForPromotion(string customerId, int promotionId);
        Task<bool> RegisterCustomerForPromotion(string customerId, int promotionId);
        Task<bool> RegisterReward(string customerId, int promotionId);
        #endregion

        #region Customer
        Task<IEnumerable<VendorViewModel>> GetCustomerEnrolledVendors(string customerId);
        Task<SalesModel> GetCustomerSalesByPromotion(string customerId, int promotionId);
        Task<IEnumerable<CustomerPromotionDashboardViewModel>> GetCustomerEnrolledPromotions(string customerId);
        Task<IEnumerable<CustomerViewModel>> GetAllCustomersEnrolledByVendorUnitAndPromotion(int vendorUnitId, int promotionId);
        Task<IEnumerable<CustomerViewModel>> GetAllCustomersEnrolledByVendorUnit(int vendorUnitId);
        Task<IEnumerable<CustomerViewModel>> GetAllCustomersEnrolledByPromotion(int promotionId);
        #endregion

        #region Promotion
        Task<bool> CreatePromotion(PromotionViewModel viewModel);
        Task<bool> UpdatePromotion(PromotionViewModel viewModel);
        #endregion
    }
}

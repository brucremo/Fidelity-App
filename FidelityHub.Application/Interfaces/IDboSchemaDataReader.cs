using FidelityHub.Application.Models.Account;
using FidelityHub.Application.Models.Authentication;
using FidelityHub.Application.Models.Registration;
using FidelityHub.Application.Models.Shared;
using FidelityHub.Database.Entities.UsrSchema;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace FidelityHub.Application.Interfaces
{
    public interface IDboSchemaDataReader
    {
        // --- Commands ---
        public Task<bool> UpdateUserForRefreshToken(AspNetUser user);
        public Task<bool> SetRefreshTokenForUser(AspNetUser user, string refreshToken, string remoteIp);
        public Task<AspNetUser> CreateNewUser(BaseRegistrationModel registrationModel, int userTypeId);
        public Task<AspNetUser> CreateThirdPartyUser(ThirdPartyRegistrationModel registrationModel);
        public Task<string> CreateResetPasswordToken(string email);
        public Task<bool> ResetPassword(ResetForgottenPasswordModel model);

        #region Queries

        #region bool
        public Task<bool> UserNameExists(string userName);
        public Task<bool> EmailExists(string email);
        public Task<bool> CheckUserPassword(AspNetUser user, string password);
        public Task<bool> IsUserRefreshTokenValid(AspNetUser user);
        public Task<bool> IsThirdPartyUserByEmail(string email);
        public Task<bool> IsResetTokenValid(string token);
        #endregion

        #region AspNetUser
        public Task<AspNetUser> GetUserByName(string userName);
        public Task<AspNetUser> GetUserById(string id);
        public Task<AspNetUser> GetUserByEmail(string email);
        #endregion

        #endregion
    }
}

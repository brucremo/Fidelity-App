using FidelityHub.Application.Interfaces;
using FidelityHub.Database.Entities.UsrSchema;
using FidelityHub.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using FidelityHub.Application.Models.Shared;
using System;
using FidelityHub.Application.Models.Registration;
using FidelityHub.Application.Models.Account;

namespace FidelityHub.Persistence
{
    public class DboSchemaDataReader : IDboSchemaDataReader
    {
        private DboDbContext Context { get; }
        private UserManager<AspNetUser> UserManager { get; }
        private readonly string ResetTokenName = "PasswordResetToken";

        public DboSchemaDataReader(DboDbContext context, UserManager<AspNetUser> userManager)
        {
            this.Context = context;
            this.UserManager = userManager;
        }

        // --- Commands ---
        public async Task<bool> UpdateUserForRefreshToken(AspNetUser user)
        {
            this.Context.Users.Update(user);
            return await this.Context.SaveChangesAsync() > 0;
        }

        public async Task<bool> SetRefreshTokenForUser(AspNetUser user, string refreshToken, string remoteIp)
        {
            var existingToken = await this.Context.RefreshTokens.FirstOrDefaultAsync(x => x.UserId == user.Id);

            if (existingToken != null)
            {
                this.Context.RefreshTokens.Remove(existingToken);
            }

            await this.Context.RefreshTokens.AddAsync(new RefreshToken(refreshToken, DateTime.UtcNow.AddDays(5), user.Id, remoteIp));
            return await this.Context.SaveChangesAsync() > 0;
        }

        public async Task<AspNetUser> CreateNewUser(BaseRegistrationModel registrationModel, int userTypeId)
        {
            var newUser = new AspNetUser
            { 
                Email = registrationModel.Email, 
                UserName = registrationModel.UserName,
                Created = DateTime.UtcNow,
                Modified = DateTime.UtcNow,
                UserTypeId = userTypeId
            };

            var identityResult = await this.UserManager.CreateAsync(newUser, registrationModel.Password);

            if (!identityResult.Succeeded)
            {
                throw new Exception("Erro ao criar usuario");
            }

            await this.Context.Users.AddAsync(newUser);

            await this.Context.SaveChangesAsync();

            return newUser;
        }

        public async Task<AspNetUser> CreateThirdPartyUser(ThirdPartyRegistrationModel registrationModel)
        {
            var newUser = new AspNetUser
            {
                Email = registrationModel.Email,
                UserName = registrationModel.Name,
                Created = DateTime.UtcNow,
                Modified = DateTime.UtcNow,
                UserTypeId = (int) Application.Enumerations.UserType.CUSTOMER,
                PhotoUrl = registrationModel.PhotoUrl
            };

            var identityResult = await this.UserManager.CreateAsync(newUser);

            if (!identityResult.Succeeded)
            {
                throw new Exception("Erro ao criar usuario");
            }

            await this.Context.Users.AddAsync(newUser);

            await this.Context.SaveChangesAsync();

            return newUser;
        }

        public async Task<string> CreateResetPasswordToken(string email)
        {
            var user = await this.GetUserByEmail(email);
            var token = await this.UserManager.GeneratePasswordResetTokenAsync(user);

            await this.Context.CustomUserTokens.AddAsync(new AspNetUserTokens
            {
                UserId = user.Id,
                LoginProvider = "",
                Name = this.ResetTokenName,
                Value = token,
                CreatedOn = DateTime.Now
            });

            await this.Context.SaveChangesAsync();

            return token;
        }

        public async Task<bool> ResetPassword(ResetForgottenPasswordModel model)
        {
            var result = await this.UserManager.ResetPasswordAsync(
                await this.Context.Users.FirstOrDefaultAsync(x => x.UserName.Trim() == model.Email.Trim()),
                model.ResetToken, 
                model.Password);

            return result.Succeeded;
        }

        #region Queries

        #region Private 

        #region bool
        #endregion

        #endregion

        #region Public 

        #region bool
        public async Task<bool> EmailExists(string email)
        {
            return await this.Context.Users.CountAsync(x => x.Email.Trim().Equals(email.Trim())) > 0;
        }

        public async Task<bool> UserNameExists(string userName)
        {
            return await this.Context.Users.CountAsync(x => x.UserName.Trim().Equals(userName.Trim())) > 0;
        }

        public async Task<bool> CheckUserPassword(AspNetUser user, string password)
        {
            return await this.UserManager.CheckPasswordAsync(user, password);
        }

        public async Task<bool> IsUserRefreshTokenValid(AspNetUser user)
        {
            var token = await this.Context.RefreshTokens.FirstOrDefaultAsync(x => x.UserId == user.Id);
            return  token != null && token.Active;
        }

        public async Task<bool> IsThirdPartyUserByEmail(string email)
        {
            return await this.Context.Users.Where(x => x.Email.ToUpper().Trim() == email.ToUpper().Trim() && x.PasswordHash == null).CountAsync() > 0;
        }

        public async Task<bool> IsResetTokenValid(string token)
        {
            var userTokens = await this.Context.CustomUserTokens.Where(x => 
                x.Value == token && 
                x.Name.Trim() == this.ResetTokenName).FirstOrDefaultAsync();

            return userTokens.CreatedOn > DateTime.Now.AddHours(-2);
        }
        #endregion

        #region AspNetUser
        public async Task<AspNetUser> GetUserByName(string userName)
        {
            return await this.Context.Users.FirstOrDefaultAsync(x => x.UserName.ToLower() == userName.ToLower());
        }

        public async Task<AspNetUser> GetUserById(string id)
        {
            return await this.Context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<AspNetUser> GetUserByEmail(string email)
        {
            return await this.Context.Users.FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());
        }
        #endregion

        #endregion

        #endregion
    }
}

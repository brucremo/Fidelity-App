using FidelityHub.Application.Interfaces;
using FidelityHub.Application.Models.Authentication;
using MediatR;
using Microsoft.JSInterop;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FidelityHub.Application.Commands.Authentication
{
    public class LoginRequest : IRequest<AccessToken>
    {
        public LoginRequestModel LoginRequestModel { get; }
        public string RemoteHostIP { get; }

        public LoginRequest(LoginRequestModel request, string remoteHostIp)
        {
            this.LoginRequestModel = request;
            this.RemoteHostIP = remoteHostIp;
        }
    }

    public class LoginRequestHandler : IRequestHandler<LoginRequest, AccessToken>
    {
        private IDboSchemaDataReader Reader { get; }
        private IJwtFactory JwtFactory { get; }
        private ITokenFactory TokenFactory { get; }

        public LoginRequestHandler(IDboSchemaDataReader reader, IJwtFactory jwtFactory, ITokenFactory tokenFactory)
        {
            this.Reader = reader;
            this.JwtFactory = jwtFactory;
            this.TokenFactory = tokenFactory;
        }

        public async Task<AccessToken> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(request.LoginRequestModel.UserName) && !string.IsNullOrEmpty(request.LoginRequestModel.Password))
            {
                // ensure we have a user with the given user name
                var user = await this.Reader.GetUserByName(request.LoginRequestModel.UserName);
                if (user != null)
                {
                    // validate password
                    if (await this.Reader.CheckUserPassword(user, request.LoginRequestModel.Password))
                    {
                        // generate refresh token
                        var refreshToken = this.TokenFactory.GenerateToken();
                        await this.Reader.SetRefreshTokenForUser(user, refreshToken, request.RemoteHostIP);
                        await this.Reader.UpdateUserForRefreshToken(user);
                        var accessToken = await this.JwtFactory.GenerateEncodedToken(user);
                        accessToken.RefreshToken = refreshToken;
                        return accessToken;
                    }
                }
            }

            throw new Exception("Invalid Username or Password");
        }
    }
}

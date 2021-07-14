using FidelityHub.Application.Interfaces;
using FidelityHub.Application.Models.Authentication;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FidelityHub.Application.Commands.Authentication
{
    public class RefreshTokenRequest : IRequest<AccessToken>
    {
        public RefreshTokenModel RefreshTokenModel { get; }
        public string SigningKey { get; }
        public string RemoteHostIP { get; }

        public RefreshTokenRequest(RefreshTokenModel refreshTokenModel, string remoteIp, string signingKey)
        {
            this.RefreshTokenModel = refreshTokenModel;
            this.RemoteHostIP = remoteIp;
            this.SigningKey = signingKey;
        }
    }

    public class RefreshTokenRequestHandler : IRequestHandler<RefreshTokenRequest, AccessToken>
    {
        private IJwtTokenValidator JwtTokenValidator { get; }
        private IDboSchemaDataReader Reader { get; }
        private IJwtFactory JwtFactory { get; }
        private ITokenFactory TokenFactory { get; }

        public RefreshTokenRequestHandler(IDboSchemaDataReader reader, IJwtFactory jwtFactory, ITokenFactory tokenFactory, IJwtTokenValidator jwtTokenValidator)
        {
            this.Reader = reader;
            this.JwtFactory = jwtFactory;
            this.TokenFactory = tokenFactory;
            this.JwtTokenValidator = jwtTokenValidator;
        }

        public async Task<AccessToken> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            var cp = this.JwtTokenValidator.GetPrincipalFromToken(request.RefreshTokenModel.AccessToken, request.SigningKey);

            // invalid token/signing key was passed and we can't extract user claims
            if (cp != null)
            {
                var id = cp.Claims.First(c => c.Type == "id");
                var user = await this.Reader.GetUserById(id.Value);

                if (await this.Reader.IsUserRefreshTokenValid(user))
                {
                    var jwtToken = await JwtFactory.GenerateEncodedToken(user);
                    var refreshToken = TokenFactory.GenerateToken();
                    //user.RemoveRefreshToken(request.RefreshTokenModel.RefreshToken); // delete the token we've exchanged
                    //user.AddRefreshToken(refreshToken, user.Id, ""); // add the new one
                    //await _userRepository.Update(user);
                    //outputPort.Handle(new ExchangeRefreshTokenResponse(jwtToken, refreshToken, true));
                    await this.Reader.SetRefreshTokenForUser(user, refreshToken, request.RemoteHostIP);
                    jwtToken.RefreshToken = refreshToken;
                    return jwtToken;
                }
            }

            throw new Exception("Erro ao atualizar tokens, cancelando login...");
        }
    }
}

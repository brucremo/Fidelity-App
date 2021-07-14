using FidelityHub.Application.Interfaces;
using FidelityHub.Application.Models.Account;
using FidelityHub.Application.Models.Authentication;
using MediatR;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FidelityHub.Application.Commands.Account
{
    public class ResetForgottenPasswordRequest : IRequest<bool>
    {
        public ResetForgottenPasswordModel ResetModel { get; }

        public ResetForgottenPasswordRequest(ResetForgottenPasswordModel model)
        {
            this.ResetModel = model;
        }
    }

    public class ResetForgottenPasswordRequestHandler : IRequestHandler<ResetForgottenPasswordRequest, bool>
    {
        private IDboSchemaDataReader Reader { get; }

        public ResetForgottenPasswordRequestHandler(IDboSchemaDataReader reader)
        {
            this.Reader = reader;
        }

        public async Task<bool> Handle(ResetForgottenPasswordRequest request, CancellationToken cancellationToken)
        {
            return await this.Reader.ResetPassword(request.ResetModel);
        }
    }
}

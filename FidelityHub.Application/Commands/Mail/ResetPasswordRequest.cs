using FidelityHub.Application.Interfaces;
using FidelityHub.Communication.Email;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FidelityHub.Application.Commands.Mail
{
    public class ResetPasswordRequest : IRequest<HttpStatusCode>
    {
        public string Email { get; set; }

        public ResetPasswordRequest(string email)
        {
            this.Email = email;
        }
    }

    public class ForgottenPasswordRequestHandler : IRequestHandler<ResetPasswordRequest, HttpStatusCode>
    {
        private IOptions<SmtpOptions> Config { get; }
        private IDboSchemaDataReader DboReader { get; }

        public ForgottenPasswordRequestHandler(IDboSchemaDataReader dboReader, IOptions<SmtpOptions> options)
        {
            this.Config = options;
            this.DboReader = dboReader;
        }

        public async Task<HttpStatusCode> Handle(ResetPasswordRequest request, CancellationToken cancellationToken)
        {
            try
            {
                MailMessage message;

                if (!await this.DboReader.IsThirdPartyUserByEmail(request.Email))
                {
                    string token = await this.DboReader.CreateResetPasswordToken(request.Email);
                    token = token.Replace('/', '|');
                    message = new MailMessage(
                                this.Config.Value.SenderAddress,
                                request.Email,
                                "Troque sua senha FidelityHub",
                                $"Olá, \n\n" +
                                $"Para trocar sua senha, por favor clique no link abaixo. " +
                                $"Este link vai te levar para uma página onde você poderá escolher uma nova senha. \n\n" +
                                $"O link expira em 2 horas após o envio desta mensagem. \n\n" +
                                $"https://fidelityhub.com.br/account/password/reset?token={token} \n\n" +
                                $"Atenciosamente, \n" +
                                $"Suporte FidelityHub \n\n\n\n" +
                                $"Esta é uma mensagem automática, por favor não responda este email.");
                }
                else
                {
                    message = new MailMessage(
                                this.Config.Value.SenderAddress,
                                request.Email,
                                "Troque sua senha FidelityHub",
                                $"Olá, \n\n" +
                                $"Aparentemente você já possui uma conta FidelityHub registrada com Google ou Facebook. " +
                                $"Por favor tente entrar com uma dessas opções em nossa tela de login. \n\n" +
                                $"Atenciosamente, \n" +
                                $"Suporte FidelityHub \n\n\n\n" +
                                $"Esta é uma mensagem automática, por favor não responda este email.");
                }

                using (var client = new CustomSmtpClient(this.Config))
                {
                    message.From = new MailAddress(this.Config.Value.SenderAddress, this.Config.Value.SenderTitle);
                    return await client.SendMessage(message)
                        ? HttpStatusCode.OK : HttpStatusCode.FailedDependency;
                }
            }
            catch(Exception ex)
            {
                return HttpStatusCode.InternalServerError;
            }
        }
    }
}

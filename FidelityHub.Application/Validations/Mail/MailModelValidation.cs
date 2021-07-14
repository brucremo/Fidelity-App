using FluentValidation;
using FidelityHub.Application.Models.Mail;

namespace FidelityHub.Application.Validations.Mail
{
    public class MailModelValidation<T> : 
        AbstractValidator<T> where T : MailModel
    {
        public MailModelValidation()
        {
            RuleFor(x => x.EmailAddress).NotEmpty();
        }
    }
}

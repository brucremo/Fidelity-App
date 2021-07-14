using FidelityHub.Application.Models.Registration;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FidelityHub.Application.Validations.Registration
{
    public class RegistrationModelValidation : AbstractValidator<VendorRegistrationModel>
    {
        public RegistrationModelValidation()
        {
            
        }
    }
}

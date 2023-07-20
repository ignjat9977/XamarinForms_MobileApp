using FluentValidation;
using Proj.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proj.Validator
{
    public class LoginPageValidation : AbstractValidator<LoginViewModel>
    {
        public LoginPageValidation()
        {
            RuleFor(x => x.Email).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Email cant be empty!")
                .EmailAddress().WithMessage("Email is not in right format!");

            var regexPassword = @"^[A-Za-z0-9!@#$%%&*()+_(]{8,}$";

            RuleFor(x => x.Password).Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Passwod cant be empty")
                .Matches(regexPassword).WithMessage("Password is not in right format!");
        }
    }
}

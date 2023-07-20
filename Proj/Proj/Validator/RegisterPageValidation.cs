using FluentValidation;
using Proj.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proj.Validator
{
    public class RegisterPageValidation : AbstractValidator<RegisterPageViewModel>
    {

        public RegisterPageValidation()
        {
            RuleFor(x => x.Email).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Email cant be empty!")
                .EmailAddress().WithMessage("Email is not in right format!");

            var regex = @"^[A-Z][a-z]{2,}(\s[A-Z][a-z]{2,})*$";

            RuleFor(x => x.FirstName).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("First Name cant be empty!")
                .Matches(regex).WithMessage("First Name is not in right format!");

            RuleFor(x => x.LastName).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Last Name cant be empty!")
                .Matches(regex).WithMessage("Last Name is not in right format!");

            var regexPassword = @"^[A-Za-z0-9!@#$%%&*()+_(]{8,}$";

            RuleFor(x => x.Password).Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Passwod cant be empty")
                .Matches(regexPassword).WithMessage("Password is not in right format!");

            var regexUsername= @"^[A-Za-z0-9!@#$%%&*()+_(]{3,12}$";

            RuleFor(x => x.Username).Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Username cant be empty")
                .Matches(regexUsername).WithMessage("Username is not in right format!");
        }
    }
}

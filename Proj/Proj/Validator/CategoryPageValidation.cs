using FluentValidation;
using Proj.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proj.Validator
{
    public class CategoryPageValidation : AbstractValidator<CategoryPageViewModel>
    {
        public CategoryPageValidation()
        {
            RuleFor(c => c.Name).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Name of category cant be empty!")
                .MinimumLength(3).WithMessage("Name of category must be longer than 3 characters!");
        }
    }
}

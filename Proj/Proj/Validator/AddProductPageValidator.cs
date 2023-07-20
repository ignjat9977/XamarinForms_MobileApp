using FluentValidation;
using Proj.DataBase;
using Proj.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proj.Validator
{
    public class AddProductPageValidator : AbstractValidator<AddProductPageVIewModel>
    {
        private Db _db;
        public AddProductPageValidator()
        {
            RuleFor(x => x.Name).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Name of product cant be empty!")
                .MinimumLength(3).WithMessage("Name must be longer than 3 character")
                .Must(ProductNameIsNotInUse).WithMessage("Product alerady in use!");

            RuleFor(x => x.Price).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Price of product cant be empty!")
                .Must(CheckPrice).WithMessage("Price of product cant lower than 0 and higher than 10000!");


            var regexImage = @"^[A-Za-z0-9?>.,<?}{|\/~`!@#$%^&*()_+]{3,}$";
            RuleFor(x => x.Image).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Image href cant be empty!")
                .Matches(regexImage).WithMessage("Image href is not in right format!");

        }
        private bool ProductNameIsNotInUse(string name)
        {
            _db = new Db();
            var product = _db.Conn.Find<Product>(x => x.Name == name);

            return product == null;
        }
        private bool CheckPrice(decimal? price)
        {
            return price > 0 && price < 10000;
        }
    }
}

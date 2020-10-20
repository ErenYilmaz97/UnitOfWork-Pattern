using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Entities;
using Entities.Entities;
using FluentValidation;

namespace Core.Validations
{
    public class ProductValidator : AbstractValidator<Product>
    {


        public ProductValidator()
        {
            RuleFor(_ => _.Name).NotNull().WithMessage("Ürün Adı Boş Olamaz").Length(2, 40)
                .WithMessage("Ürün Adı 2-40 Karakter Arası Olmalı");

            RuleFor(_ => _.Price).NotNull().WithMessage("Ürün Fiyatı Boş Olamaz").GreaterThan(0)
                .WithMessage("Ürün Fiyatı 0 TL Olamaz");

            RuleFor(_ => _.Stock).NotNull().WithMessage("Ürün Stok Bilgisi Boş Olamaz.").GreaterThan(0)
                .WithMessage("Ürün Stok Bilgisi 0 Olamaz");

            RuleFor(_ => _.CategoryID).NotNull().WithMessage("CategoryID Boş Olamaz").GreaterThan(0)
                .WithMessage("CategoryID 0 Olamaz");
        }


        
    }
}

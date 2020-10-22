using System.Collections.Generic;
using Entities.Entities;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation.Validators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("Ürün Adı Boş Olamaz.").Length(2, 50)
                .WithMessage("Ürün Adı 2-50 Karakter Olmalıdır.");


            RuleFor(x => x.Price).NotNull().WithMessage("Ürün Fiyatı Boş Olamaz.").GreaterThan(0)
                .WithMessage("Ürün Fiyatı 0 TLden Fazla Olmalıdır.");


            RuleFor(x => x.Stock).NotNull().WithMessage("Ürün Stoğu Boş Olamaz.").GreaterThan(0)
                .WithMessage("Ürün Stoğu 0'dan Büyük Olmalıdır.");

            RuleFor(x => x.CategoryID).NotNull().WithMessage("Kategori ID Boş Olamaz.").GreaterThan(0)
                .WithMessage("Kategori ID 0'dan Büyük Olmaldıır.");

            

        }
    }
}

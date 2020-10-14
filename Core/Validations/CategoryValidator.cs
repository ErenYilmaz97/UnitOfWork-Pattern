using System;
using System.Collections.Generic;
using System.Text;
using Entities.Entities;
using FluentValidation;

namespace Core.Validations
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("Kategori Adı Boş Olamaz").Length(2, 30)
                .WithMessage("Kategori Adı 2-30 Karakter Olmalı");
        }
    }
}

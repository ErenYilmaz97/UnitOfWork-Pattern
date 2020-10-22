using Entities.Entities;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation.Validators
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("Kategori Adı Boş Olamaz.").Length(2, 30)
                .WithMessage("Kategori Adı 2-30 Karakter Uzunluğunda Olmalıdır.");
        }
    }
}
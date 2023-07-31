using EBusinessEntity.Entities;
using FluentValidation;

namespace EBusinessService.FluentValidations
{
    public class BlogValidator:AbstractValidator<Blog>
    {
        public BlogValidator()
        {
            RuleFor(b => b.Name).NotEmpty().NotNull().MinimumLength(3).MaximumLength(100).WithName("Kateqoriya adı");
        }
    }
}

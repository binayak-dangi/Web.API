using FluentValidation;
using Web.API.Models.DTOS.Setup;

namespace Web.API.Validators
{
    public class AdmElementValidator : AbstractValidator<AdmElementDto>
    {
        public AdmElementValidator()
        {
            RuleFor(x => x.ElementHead)
                .NotEmpty()
                .WithMessage("Element Head is required.")
                .MaximumLength(100);

        }
    }
}
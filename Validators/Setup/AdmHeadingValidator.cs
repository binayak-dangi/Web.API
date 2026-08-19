using FluentValidation;
using Web.API.Models.DTOS.Setup;

namespace Web.API.Validators
{
    public class AdmHeadingValidator : AbstractValidator<AdmHeadingDto>
    {
        public AdmHeadingValidator()
        {
            RuleFor(x => x.Heading)
                .NotEmpty()
                .WithMessage("Heading is required.")
                .MaximumLength(100);

        }
    }
}
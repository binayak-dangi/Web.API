using FluentValidation;
using Web.API.Models.DTOS.Setup;

namespace Web.API.Validators
{
    public class AdmMainHeadingValidator : AbstractValidator<AdmMainHeadingDto>
    {
        public AdmMainHeadingValidator()
        {
            RuleFor(x => x.MainHeading)
                .NotEmpty()
                .WithMessage("Main Heading is required.")
                .MaximumLength(100);

        }
    }
}
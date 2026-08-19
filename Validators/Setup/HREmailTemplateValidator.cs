using FluentValidation;
using Web.API.Models.DTOS.Setup;

namespace Web.API.Validators
{
    public class HREmailTemplateValidator : AbstractValidator<HREmailTemplateDto>
    {
        public HREmailTemplateValidator()
        {
            RuleFor(x => x.TemplateName)
                .NotEmpty()
                .WithMessage("Template Name is required.")
                .MaximumLength(100);

        }
    }
}
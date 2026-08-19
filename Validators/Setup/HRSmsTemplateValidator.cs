using FluentValidation;
using Web.API.Models.DTOS.Setup;

namespace Web.API.Validators
{
    public class HRSmsTemplateValidator : AbstractValidator<HRSmsTemplateDto>
    {
        public HRSmsTemplateValidator()
        {
            RuleFor(x => x.TemplateName)
                .NotEmpty()
                .WithMessage("Template Name is required.")
                .MaximumLength(100);

        }
    }
}
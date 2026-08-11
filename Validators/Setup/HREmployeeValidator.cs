using FluentValidation;
using Web.API.Models.DTOS.Setup;

namespace Web.API.Validators.Setup
{
    public class HREmployeeValidator : AbstractValidator<HREmployeeDto>
    {
        public HREmployeeValidator()
        {
            RuleFor(x => x.Username)
                .MinimumLength(5)
                .WithMessage("Username must be at least 5 characters long.");
        }
    }
}
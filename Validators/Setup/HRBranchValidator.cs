using FluentValidation;
using Web.API.Models.DTOS.Setup;

namespace Web.API.Validators
{
    public class HRBranchValidator : AbstractValidator<HRBranchDto>
    {
        public HRBranchValidator()
        {
            RuleFor(x => x.BranchName)
                .NotEmpty()
                .WithMessage("Branch Name is required.")
                .MaximumLength(100);

            RuleFor(x => x.BranchAddress)
                .NotEmpty()
                .WithMessage("Branch Address is required.")
                .MaximumLength(250);

            RuleFor(x => x.PhoneNo)
                .NotEmpty()
                .WithMessage("Phone Number is required.")
                .MaximumLength(10)
                .Matches(@"^[0-9+\-\s()]*$")
                .When(x => !string.IsNullOrWhiteSpace(x.PhoneNo))
                .WithMessage("Invalid phone number.");

            RuleFor(x => x.BMEmailID)
                 .NotEmpty()
                .WithMessage("Email Address is required.")
                .EmailAddress()
                .When(x => !string.IsNullOrWhiteSpace(x.BMEmailID))
                .WithMessage("Invalid email address.");
        }
    }
}
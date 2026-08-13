namespace Web.API.Models.DTOS.Setup
{
    public class CompleteFirstPasswordDto
    {
        public string Username { get; set; } = null!;

        public string CurrentPassword { get; set; } = null!;

        public string NewPassword { get; set; } = null!;

        public string ConfirmNewPassword { get; set; } = null!;
    }

    public enum PasswordSetupResult
    {
        Success,
        EmployeeNotFound,
        NotNewlyAdded,
        InvalidCurrentPassword,
        PasswordMismatch,
        NewPasswordSameAsCurrent
    }
}
namespace Web.API.Models.DTOS.Setup
{
    public class ChangePasswordDto
    {
        public string Username { get; set; } = null!;
        public string CurrentPassword { get; set; } = null!;
        public string NewPassword { get; set; } = null!;
        public string ConfirmNewPassword { get; set; } = null!;

    }
}
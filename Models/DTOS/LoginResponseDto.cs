namespace Web.API.Models.DTOS
{
    public class LoginResponseDto
    {
        public string AccessToken { get; set; } = string.Empty;

        public string RefreshToken { get; set; } = string.Empty;

        public DateTime ExpiresAt { get; set; }

        public HREmployeeDto Employee { get; set; } = null!;
    }
}

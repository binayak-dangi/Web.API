namespace Web.API.Models.DTOS
{
    public class RefreshTokenRequestDto
    {
        public long Id { get; set; }

        public long IDHREmployee { get; set; }

        public string Token { get; set; } = string.Empty;

        public DateTime Expires { get; set; }

        public bool IsRevoked { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Revoked { get; set; }
    }
}

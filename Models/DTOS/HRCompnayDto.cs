namespace Web.API.Models.DTOS
{
    public class HRCompnayDto
    {
        public long Id { get; set; }
        public string CompanyName { get; set; } = null!;
        public long LoginAttempt { get; set; }
        public bool IsActive { get; set; }
    }
}

namespace Web.API.Models.DTOS
{
    public class HRCompanyDto
    {
        public long Id { get; set; }
        public string CompanyName { get; set; } = null!;
        public int LoginAttempt { get; set; }
        public bool IsActive { get; set; }
    }
}

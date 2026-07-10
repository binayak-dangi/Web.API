namespace Web.API.Models.DTOS
{
    public class HRBranchDto
    {
        public long Id { get; set; }

        public string BranchName { get; set; } = null!;

        public string BranchAddress { get; set; } = null!;

        public string? PhoneNo { get; set; }

        public string? BMEmailID { get; set; }

        public bool IsActive { get; set; }
    }
}

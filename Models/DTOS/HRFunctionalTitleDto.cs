namespace Web.API.Models.DTOS
{
    public class HRFunctionalTitleDto
    {
        public long Id { get; set; }

        public string PositionHead { get; set; } 

        public string PositionDescription { get; set; } = string.Empty;

        public string PositionCode { get; set; } = string.Empty;

        public long PositionOrder { get; set; }

        public string ClientIP { get; set; } = string.Empty;
        public long IdHRCompany { get; set; }
        public bool IsActive { get; set; }
    }
}

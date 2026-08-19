namespace Web.API.Models.DTOS.Setup
{
    public class HRSmsTemplateDto
    {
        public long Id { get; set; }
        public long IdHRCompany { get; set; }

        public string TemplateName { get; set; } = null!;

        public string TemplateCode { get; set; } = null!;

        public string Message { get; set; } = null!;

        public string? Description { get; set; }

        public long Priority { get; set; }

        public bool isActive { get; set; }
    }
}

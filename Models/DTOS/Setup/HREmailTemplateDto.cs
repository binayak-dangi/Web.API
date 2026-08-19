namespace Web.API.Models.DTOS.Setup
{
    public class HREmailTemplateDto
    {
        public long Id { get; set; }
        public string TemplateName { get; set; } = null!;

        public string Subject { get; set; } = null!;

        public string Body { get; set; } = null!;

        public bool IsHTML { get; set; }

        public bool isActive { get; set; }
        public long IdHRCompany { get; set; }
    }
}

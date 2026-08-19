namespace Web.API.Models.DTOS.Setup
{
    public class AdmHeadingDto
    {
        public long Id { get; set; }
        public string Heading { get; set; } = string.Empty;

        public long IdMainHeading { get; set; }

        public string? HeadingDescription { get; set; }

        public string HeadingCode { get; set; } = string.Empty;

        public bool IsSystemDefined { get; set; }

        public string ClientIP { get; set; } = string.Empty;

        public long OrderNo { get; set; }

        public bool IsActive { get; set; }

    }
}

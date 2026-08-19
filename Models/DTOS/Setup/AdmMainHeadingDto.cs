using Web.API.Models.Entities.Setup;

namespace Web.API.Models.DTOS.Setup
{
    public class AdmMainHeadingDto
    {
        public long Id { get; set; }
        public string MainHeading { get; set; } = string.Empty;

        public string Abbr { get; set; } = string.Empty;

        public string? MainHeadingDescription { get; set; }

        public bool IsSystemDefined { get; set; }

        public long OrderNo { get; set; }
        public bool IsActive { get; set; }

    }
}

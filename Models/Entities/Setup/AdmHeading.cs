using System.ComponentModel.DataAnnotations.Schema;

namespace Web.API.Models.Entities.Setup
{
    [Table("Adm_Heading")]
    public class AdmHeading: CommonModel
    {
        public string Heading { get; set; } = string.Empty;

        public long IdMainHeading { get; set; }

        public string? HeadingDescription { get; set; }

        public string HeadingCode { get; set; } = string.Empty;

        public bool IsSystemDefined { get; set; }

        public string ClientIP { get; set; } = string.Empty;

        public long OrderNo { get; set; }

        [ForeignKey("IdMainHeading")]
        public AdmMainHeading? MainHeading { get; set; }

        public ICollection<AdmElement>? Adm_Element { get; set; } = new List<AdmElement>(); 
    }
}
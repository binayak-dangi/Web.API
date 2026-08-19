using System.ComponentModel.DataAnnotations.Schema;

namespace Web.API.Models.Entities.Setup
{
    [Table("Adm_MainHeading")]
    public class AdmMainHeading:CommonModel
    {
        public string MainHeading { get; set; } = string.Empty;

        public string Abbr { get; set; } = string.Empty;

        public string? MainHeadingDescription { get; set; }

        public bool IsSystemDefined { get; set; }

        public long OrderNo { get; set; }

        public ICollection<AdmHeading>? Adm_Heading { get; set; }= new List<AdmHeading>();
    }
}
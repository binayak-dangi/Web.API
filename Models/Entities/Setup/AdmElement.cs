using System.ComponentModel.DataAnnotations.Schema;

namespace Web.API.Models.Entities.Setup
{
    [Table("Adm_Element")]
    public class AdmElement:CommonModel
    {
        public long IdHeading { get; set; }

        public string ElementHead { get; set; } = string.Empty;

        public string? CatalogueCode { get; set; }

        public string? ElementDescription { get; set; }

        public bool IsSystemDefined { get; set; }

        public long IdParentElement { get; set; }

        public string ClientIP { get; set; } = string.Empty;

        public long OrderNo { get; set; }

        [ForeignKey("IdHeading")]
        public AdmHeading? Heading { get; set; }
    }
}
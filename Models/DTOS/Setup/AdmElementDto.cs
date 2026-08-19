namespace Web.API.Models.DTOS.Setup
{
    public class AdmElementDto
    {
        public long Id { get; set; }
        public long IdHeading { get; set; }

        public string ElementHead { get; set; } = string.Empty;

        public string? CatalogueCode { get; set; }

        public string? ElementDescription { get; set; }

        public bool IsSystemDefined { get; set; }

        public long IdParentElement { get; set; }

        public string ClientIP { get; set; } = string.Empty;

        public long OrderNo { get; set; }

        public bool IsActive { get; set; }
    }
}

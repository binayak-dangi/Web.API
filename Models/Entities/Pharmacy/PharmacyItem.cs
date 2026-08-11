namespace Web.API.Models.Entities.Pharmacy
{
    public class PharmacyItem:CommonModel
    {
        public long IdHRCompany { get; set; }

        public string ItemCode { get; set; } = string.Empty;

        public string? Barcode { get; set; }

        public string Title { get; set; } = string.Empty;

        public string? GenericName { get; set; }

        public long IdCategory { get; set; }

        public long? IdManufacturer { get; set; }

        public long IdUnit { get; set; }

        public decimal ReorderLevel { get; set; } = 0;

        public bool IsMedicine { get; set; } = true;

        public bool RequiresPrescription { get; set; } = false;
    }
}

namespace Web.API.Models.Entities.Pharmacy
{
    public class PharmacySupplier:CommonModel
    {
        public long IdHRCompany { get; set; }

        public string? SupplierCode { get; set; }

        public string SupplierName { get; set; } = string.Empty;

        public string? ContactPerson { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public string? Address { get; set; }

    }

}

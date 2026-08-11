namespace Web.API.Models.Entities.Pharmacy
{
    public class PharmacyWarehouse:CommonModel
    {
        public long IdHRCompany { get; set; }

        public long IdHRBranch { get; set; }

        public string WarehouseName { get; set; } = string.Empty;

        public string? Address { get; set; }

    }

}

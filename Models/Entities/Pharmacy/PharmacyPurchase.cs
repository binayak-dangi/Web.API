namespace Web.API.Models.Entities.Pharmacy
{
    public class PharmacyPurchase:CommonModel
    {
        public long IdHRCompany { get; set; }

        public long IdHRBranch { get; set; }

        public string PurchaseNo { get; set; } = string.Empty;

        public DateTime PurchaseDate { get; set; }

        public long IdSupplier { get; set; }

        public decimal SubTotal { get; set; } = 0;

        public decimal Discount { get; set; } = 0;

        public decimal Tax { get; set; } = 0;

        public decimal NetAmount { get; set; } = 0;

        public string? Status { get; set; }
    }

}

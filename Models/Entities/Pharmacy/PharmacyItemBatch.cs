namespace Web.API.Models.Entities.Pharmacy
{
    public class PharmacyItemBatch:CommonModel
    {
        public long IdItem { get; set; }

        public string BatchNo { get; set; } = string.Empty;

        public DateTime? ManufactureDate { get; set; }

        public DateTime ExpiryDate { get; set; }

        public decimal PurchasePrice { get; set; }

        public decimal SellingPrice { get; set; }

        public decimal? MRP { get; set; }
    }
}

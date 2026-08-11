namespace Web.API.Models.Entities.Pharmacy
{
    public class PharmacyPurchaseItem:CommonModel
    {
        public long IdPurchase { get; set; }

        public long IdItemBatch { get; set; }

        public decimal Quantity { get; set; }

        public decimal FreeQuantity { get; set; } = 0;

        public decimal PurchasePrice { get; set; }

        public decimal Discount { get; set; } = 0;

        public decimal Total { get; set; }
    }

}

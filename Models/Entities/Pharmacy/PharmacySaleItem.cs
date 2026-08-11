namespace Web.API.Models.Entities.Pharmacy
{
    public class PharmacySaleItem
    {
        public long Id { get; set; }

        public long IdSale { get; set; }

        public long IdItemBatch { get; set; }

        public decimal Quantity { get; set; }

        public decimal SellingPrice { get; set; }

        public decimal Discount { get; set; } = 0;

        public decimal Total { get; set; }
    }

}

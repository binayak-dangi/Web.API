namespace Web.API.Models.Entities.Pharmacy
{
    public class PharmacyStockLedger
    {
        public long Id { get; set; }

        public long IdWarehouse { get; set; }

        public long IdItemBatch { get; set; }

        public string TransactionType { get; set; } = string.Empty;

        public long? ReferenceId { get; set; }

        public decimal QuantityIn { get; set; } = 0;

        public decimal QuantityOut { get; set; } = 0;

        public DateTime TransactionDate { get; set; } = DateTime.Now;

        public string? Remarks { get; set; }

        public long? CreatedBy { get; set; }
    }

}

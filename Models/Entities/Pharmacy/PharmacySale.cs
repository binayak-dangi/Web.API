namespace Web.API.Models.Entities.Pharmacy
{
    public class PharmacySale:CommonModel
    {
        public long IdHRCompany { get; set; }

        public long IdHRBranch { get; set; }

        public string InvoiceNo { get; set; } = string.Empty;

        public DateTime SaleDate { get; set; }

        public long? IdCustomer { get; set; }

        public decimal SubTotal { get; set; } = 0;

        public decimal Discount { get; set; } = 0;

        public decimal Tax { get; set; } = 0;

        public decimal NetAmount { get; set; } = 0;

        public string? PaymentMethod { get; set; }

    }

}

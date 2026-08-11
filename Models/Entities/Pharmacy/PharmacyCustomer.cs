namespace Web.API.Models.Entities.Pharmacy
{
    public class PharmacyCustomer:CommonModel
    {
        public long IdHRCompany { get; set; }

        public string? CustomerCode { get; set; }

        public string CustomerName { get; set; } = string.Empty;

        public string? Phone { get; set; }

        public string? Address { get; set; }

    }

}

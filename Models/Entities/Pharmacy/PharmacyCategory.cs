namespace Web.API.Models.Entities.Pharmacy
{
    public class PharmacyCategory:CommonModel
    {
        public long IdHRCompany { get; set; }

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

    }
}

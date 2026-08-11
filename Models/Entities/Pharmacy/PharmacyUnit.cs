namespace Web.API.Models.Entities.Pharmacy
{
    public class PharmacyUnit:CommonModel
    {
        public string Title { get; set; } = string.Empty;

        public string? ShortName { get; set; }

    }
}

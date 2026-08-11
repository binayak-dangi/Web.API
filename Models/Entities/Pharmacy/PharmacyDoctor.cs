namespace Web.API.Models.Entities.Pharmacy
{
    public class PharmacyDoctor:CommonModel
    {
        public long IdHRCompany { get; set; }

        public string? DoctorCode { get; set; }

        public string DoctorName { get; set; } = string.Empty;

        public string? Specialization { get; set; }

        public string? LicenseNo { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public string? Address { get; set; }
    }
}

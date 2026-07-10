using MyTestMvc.Models;
using MyTestMvc.Models.Setup;
using System;

namespace Web.API.Entities
{
    public class HRCorporateTitle:CommonModel
    {

        public string LevelGrade { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal MinBasicSalary { get; set; } 

        public decimal MaxBasicSalary { get; set; }

        public decimal MinAllowance { get; set; }

        public decimal MaxVehicleAllowance { get; set; }

        public decimal? MaxAllowance { get; set; }

        public string ShortName { get; set; } = string.Empty;

        public string ClientIP { get; set; } = string.Empty;
        public long IdHRCompany { get; set; }

        public virtual ICollection<HREmployee> HREmployees { get; set; } = new List<HREmployee>();
    }
}
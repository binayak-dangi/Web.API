using MyTestMvc.Models;
using MyTestMvc.Models.Setup;
using System;

namespace Web.API.Entities
{
    public class HRFunctionalTitle:CommonModel
    {

        public string PositionHead { get; set; } = string.Empty;

        public string PositionDescription { get; set; } = string.Empty;

        public string PositionCode { get; set; } = string.Empty;

        public long PositionOrder { get; set; }

        public string ClientIP { get; set; } = string.Empty;
        public long IdHRCompany { get; set; }


        public virtual ICollection<HREmployee> HREmployees { get; set; } = new List<HREmployee>();
    }
}
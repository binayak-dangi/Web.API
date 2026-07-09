using System;
using System.ComponentModel.DataAnnotations;

namespace MyTestMvc.Models.Common
{
    public class PaginationModel
    {
        //public object Id { get; set; }
        //public int IdHRCompany { get; set; }
        //public int IDHREmployee { get; set; }
        //public int IdHRBranch { get; set; } = 9;
        public int? page { get; set; } = 1;
        public int? pagesize { get; set; } = 10;
        public string? sort { get; set; } = "Id";
        public string? sortdir { get; set; } = "DESC";
        public string? searchKey { get; set; } = "";
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace Web.API.Models.Common
{
    public class PaginationModel
    {
        public int? Page { get; set; } = 1;
        public int? PageSize { get; set; } = 10;
        public string? Sort { get; set; } = "Id";
        public string? SortDir { get; set; } = "DESC";
        public string? SearchKey { get; set; } = "";
    }
}

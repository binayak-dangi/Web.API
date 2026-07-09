
namespace MyTestMvc.Models.Common
{
    public class SearchFilterModel
    {
        public bool isReadOnly { get; set; }

        public string? status { get; set; }

        public string? searchKey { get; set; }

       
        public DateTime? fromDate { get; set; }

        public DateTime? toDate { get; set; }
    }
}
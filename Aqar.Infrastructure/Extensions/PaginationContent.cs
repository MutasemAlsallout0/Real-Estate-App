using Aqar.Data.Model;

namespace Aqar.Infrastructure.Extensions
{
    public class PaginationContent
    {
        public List<Estate> GetEstates { get; set; }


        public string NameSortOrder { get; set; }
        
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string Term { get; set; }
        public string OrderBy { get; set; }
    }
}

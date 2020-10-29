using TechLibrary.Contracts.Responses;
using TechLibrary.Interfaces;

namespace TechLibrary.Interfaces
{
    public interface ISearchRequest
    {
        CategorySearchResponse[] Categorys { get; set; }
        int? Page { get; set; }
        int? Pages { get; set; }
        int? RecordsPerPage { get; set; }
        string SearchString { get; set; }

        bool NewSearch { get; set; }
    }
}
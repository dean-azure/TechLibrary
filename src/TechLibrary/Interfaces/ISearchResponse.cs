using TechLibrary.Contracts.Responses;

namespace TechLibrary.Interfaces
{
    public interface ISearchResponse
    {
        int? Page { get; set; }
        int Pages { get; }
        int[] PageNumbers { get; }
        int? RecordCount { get; set; }
        int? RecordsPerPage { get; set; }

        string SearchString { get; set; }

        BookSearchResponse[] SearchResults { get; set; }
        CategorySearchResponse[] Categorys { get; set; }
    }
}

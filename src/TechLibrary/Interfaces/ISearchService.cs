using System.Threading.Tasks;
using TechLibrary.Contracts.Requests;
using TechLibrary.Contracts.Responses;

namespace TechLibrary.Services
{
    public interface ISearchService
    {
        Task<SearchResponse> GetBooksAsync(SearchRequest searchRequest);
    }
}
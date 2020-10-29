using System.Threading.Tasks;
using TechLibrary.Contracts.Requests;
using TechLibrary.Domain;

namespace TechLibrary.Interfaces
{
    public interface IBookService
    {
        Task<Book> AddBook(BookRequest newBook);
        Task<Book> EditBook(BookRequest editBook);
        Task<Book> GetBookByIdAsync(int bookid);
        Task DeleteBook(int bookId);
    }
}
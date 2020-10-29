using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using TechLibrary.Domain;
using TechLibrary.Services;
using TechLibrary.Interfaces;
using TechLibrary.Contracts.Requests;
using System.Linq;
using TechLibrary.Contracts.Responses;

namespace TechLibrary.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : Controller
    {
        private readonly ILogger<BooksController> _logger;
        private readonly IBookService _bookService;
        private readonly ISearchService _searchService;
        private readonly IMapper _mapper;

        public BooksController(ILogger<BooksController> logger, IBookService bookService, ISearchService searchService, IMapper mapper)
        {
            _logger = logger;
            _bookService = bookService;
            _searchService = searchService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Get all books");

            SearchRequest searchRequest = new SearchRequest()
            {
                NewSearch = true,
                Page = 1
            };

            var searchResponse = await _searchService.GetBooksAsync(searchRequest);

            searchResponse.Books = searchResponse.Books.Select(result => _mapper.Map<BookSearchResponse>(result)).ToArray();

            return Ok(searchResponse);
        }

        [HttpPut]
        public async Task<IActionResult> SearchBooks([FromBody] SearchRequest searchRequest = null)
        {
            _logger.LogInformation("Search all books (PUT)");

            searchRequest = searchRequest ?? new SearchRequest()
            {
                NewSearch = true,
                Page = 1
            };

            var searchResponse = await _searchService.GetBooksAsync(searchRequest);

            searchResponse.Books = searchResponse.Books.Select(result => _mapper.Map<BookSearchResponse>(result)).ToArray();

            return Ok(searchResponse);

        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation($"Get book by id {id}");

            var book = await _bookService.GetBookByIdAsync(id);

            var bookResponse = _mapper.Map<BookEditResponse>(book);
            var o = book.BookCategories.Select(bc => bc.Category).ToArray();
            bookResponse.Categories = book.BookCategories.Select(bc => new Category() { Id = bc.Category.Id, CategoryName = bc.Category.CategoryName } ).ToArray();

            return Ok(bookResponse);
        }
    }
}

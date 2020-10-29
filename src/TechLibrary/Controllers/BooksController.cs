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
using System.Net;
using System;

namespace TechLibrary.Controllers
{
    [ApiController]
    [Route("api/books")]
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
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            _logger.LogInformation($"Get book by id {id}");

            var book = await _bookService.GetBookByIdAsync(id);

            var bookResponse = _mapper.Map<BookEditResponse>(book);
            var o = book.BookCategories.Select(bc => bc.Category).ToArray();
            bookResponse.Categories = book.BookCategories.Select(bc => new Category() { Id = bc.Category.Id, CategoryName = bc.Category.CategoryName } ).ToArray();

            return Ok(bookResponse);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook([FromRoute] int id)
        {
            try
            {
                _logger.LogInformation($"Delete book by id {id}");

                await _bookService.DeleteBook(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new BookEditResponse() { ErrorMessage = ex.Message });
            }

        }

        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] BookRequest bookRequst)
        {
            try
            {
                _logger.LogInformation($"Add book");

                var newBook = await _bookService.AddBook(bookRequst);

                return StatusCode((int)HttpStatusCode.Created,  _mapper.Map<BookEditResponse>(newBook));
            }
            catch (Exception ex)
            {
                var response = _mapper.Map<BookEditResponse>(bookRequst);
                response.ErrorMessage = ex.Message;
                return StatusCode((int)HttpStatusCode.BadRequest, response);
            }
        }

        [HttpPut("id")]
        public async Task<IActionResult> EditBook([FromRoute] int id, [FromBody] BookRequest bookRequst)
        {
            if (id != bookRequst.Id.Value)
            {
                return BadRequest(new BookEditResponse() { ErrorMessage = "The book id does not match the route's id" });
            }

            try
            {
                _logger.LogInformation($"Add book");

                var editedBook = await _bookService.EditBook(bookRequst);

                return StatusCode((int)HttpStatusCode.Accepted, _mapper.Map<BookEditResponse>(editedBook));
            }
            catch (Exception ex)
            {
                var response = _mapper.Map<BookEditResponse>(bookRequst);
                response.ErrorMessage = ex.Message;
                return StatusCode((int)HttpStatusCode.BadRequest, response);
            }
        }

    }
}

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using TechLibrary.Contracts.Requests;
using TechLibrary.Contracts.Responses;
using TechLibrary.Data;
using TechLibrary.Domain;
using TechLibrary.Interfaces;

namespace TechLibrary.Services
{
    public class SearchService : ISearchService
    {
        private readonly BooksContext _dataContext;
        private readonly IAppConfig _appConfig;
        private readonly IMapper _mapper;

        public SearchService(BooksContext dataContext, IMapper mapper, IAppConfig appConfig)
        {
            _dataContext = dataContext;
            _appConfig = appConfig;
            _mapper = mapper;
        }

        public async Task<SearchResponse> GetBooksAsync(SearchRequest searchRequest)
        {

            var response = new SearchResponse();

            if (searchRequest.NewSearch)
            {
                searchRequest.Page = 1;
                searchRequest.RecordsPerPage = searchRequest.RecordsPerPage;
            }

            var categoryIdSearch = searchRequest.Categories.Where(x => x.Selected).Select(x=> x.Id).ToArray();

            if (!string.IsNullOrWhiteSpace(searchRequest.SearchString) && categoryIdSearch.Count() > 0)
            {
                var bookCategories = await (from bc in _dataContext.BookCategories //_dataContext.BookCategories
                          .Include(c => c.Category)
                          .Include(b => b.Book)
                            join id in categoryIdSearch on bc.CategoryId equals id
                            where
                             (bc.Book.Title != null && bc.Book.Title.ToLower().Contains(searchRequest.SearchString.ToLower()))
                             ||
                             (bc.Book.LongDescr != null && bc.Book.LongDescr.ToLower().Contains(searchRequest.SearchString.ToLower()))
                             ||
                             (bc.Book.ISBN != null && bc.Book.ISBN.ToLower().Contains(searchRequest.SearchString.ToLower()))
                        select bc).ToArrayAsync();

                response = CreateResponse(searchRequest, bookCategories);
            }
            else if (!string.IsNullOrWhiteSpace(searchRequest.SearchString))
            {

                var books =  await (from book in _dataContext.Books
                         .Include(bc => bc.BookCategories)
                         .ThenInclude(c => c.Category)
                             where 
                             (book.Title!=null && book.Title.ToLower().Contains(searchRequest.SearchString.ToLower()))
                             ||
                             (book.LongDescr != null && book.LongDescr.ToLower().Contains(searchRequest.SearchString.ToLower()))
                             ||
                             (book.ISBN != null && book.ISBN.ToLower().Contains(searchRequest.SearchString.ToLower()))
                        select book).ToArrayAsync();


                response = CreateResponse(searchRequest, books);
            }
            else if (categoryIdSearch.Count() > 0)
            {
                var bookCategories = await (from bc in _dataContext.BookCategories
                          .Include(c => c.Category)
                          .Include(b => b.Book)
                                        join id in categoryIdSearch on bc.CategoryId equals id
                                select bc)
                                .Skip(searchRequest.Skip).Take(searchRequest.RecordsPerPage).ToArrayAsync();

                response = CreateResponse(searchRequest, bookCategories);
            }
            else 
            {

                var books = await (from book in _dataContext.Books
                         .Include(bc => bc.BookCategories)
                         .ThenInclude(c => c.Category)
                                   select book).ToArrayAsync();
                
                response = CreateResponse(searchRequest, books);
            }


            return response;
        }

        private SearchResponse CreateResponse(SearchRequest searchRequest, IEnumerable<BookCategory> bookCategories )
        {
            var searchResponse = new SearchResponse()
            {
                RecordCount = bookCategories.Select(bc => bc.Book).Count(),
                RecordsPerPage = searchRequest.RecordsPerPage,
                Page=searchRequest.Page,
                SearchString=searchRequest.SearchString
            };

            searchResponse.Categories = bookCategories.GroupBy(bc => bc.CategoryId)
                .Select(grp =>
                    new CategorySearchResponse()
                    {
                        Id = grp.Key,
                        CategoryName = grp.Select(bc => bc.Category.CategoryName).FirstOrDefault(),
                        Count = grp.Count(),
                        Selected = searchRequest.Categories.Any(cat => cat.Id == grp.Key && cat.Selected)
                    }).OrderByDescending(bc => bc.Count).ToArray();

            Book b = new Book();
            _mapper.Map(b, new BookSearchResponse(_appConfig));

            searchResponse.Books = bookCategories
                .Skip(searchRequest.Skip)
                .Take(searchRequest.RecordsPerPage)
                .Select(bc => _mapper.Map(bc.Book, new BookSearchResponse(_appConfig))  )
                .ToArray();


            return searchResponse;
        }

        private SearchResponse CreateResponse(SearchRequest searchRequest, IEnumerable<Book> books)
        {
            var searchResponse = new SearchResponse()
            {
                RecordCount = books.Count(),
                RecordsPerPage = searchRequest.RecordsPerPage,
                Page = searchRequest.Page,
                SearchString = searchRequest.SearchString
            };

            searchResponse.Categories = books.SelectMany(b => b.BookCategories).GroupBy(bc => bc.CategoryId)
                .Select(grp =>
                    new CategorySearchResponse()
                    {
                        Id = grp.Key,
                        CategoryName = grp.Select(bc => bc.Category.CategoryName).FirstOrDefault(),
                        Count = grp.Count(),
                        Selected = searchRequest.Categories.Any(cat => cat.Id == grp.Key && cat.Selected)
                    }).OrderByDescending(bc=> bc.Count).ToArray();


            searchResponse.Books = books
                .Skip(searchRequest.Skip)
                .Take(searchRequest.RecordsPerPage)
                .Select(b => _mapper.Map(b, new BookSearchResponse(_appConfig))).ToArray();

            return searchResponse;
        }

    }
}

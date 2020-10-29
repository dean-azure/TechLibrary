using System;
using System.Collections.Generic;
using System.Text;

namespace TechLibrary.Test.Controllers
{
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Moq;
    using NUnit.Framework;
    using System.Linq;
    using System.Threading.Tasks;
    using TechLibrary.Contracts.Requests;
    using TechLibrary.Controllers;
    using TechLibrary.Data;
    using TechLibrary.Domain;
    using TechLibrary.Interfaces;
    using TechLibrary.Services;
    using TechLibrary.Test.Common;

    [TestFixture()]
    [Category("EFTests")]
    [Category("IntegrationTests")]
    public class EFTests
    {
        const string _dbFilePath = "..\\..\\..\\..\\..\\TechLibrary\\techLibrary.db";
        private Mock<ILogger<BooksContext>> _mockLogger;
        private IBookService _bookService;
        private IMapper _mapper;
        private NullReferenceException _expectedException;
        private IAppConfig _appConfig;

        [OneTimeSetUp]
        public void TestSetup()
        {
            _expectedException = new NullReferenceException("Test Failed...");
            _mockLogger = LoggingTestFixture.CreateMockLogger<BooksContext>();

            _mapper = AutoMapperUtility.CreateAutoMapper();
            _appConfig = MockAppConfig.CreateMockAppConfig();
        }

        
        [Test()]
        public async Task GetBooksThenSkipPage1()
        {

            var searchService = new SearchService(new BooksContext(_mockLogger.Object, "Data Source=" + _dbFilePath), _mapper, MockAppConfig.CreateMockAppConfig());

            var searchRequest = new SearchRequest()
            {
                Page = 10,
                RecordsPerPage = 20,
                SearchString = "sql"
            };
            searchRequest.NewSearch = true;

            Assert.AreEqual(searchRequest.Page, 1);

            var firstResponse = await searchService.GetBooksAsync(searchRequest);

            Assert.AreEqual(firstResponse.RecordsPerPage, searchRequest.RecordsPerPage);
            Assert.AreEqual(firstResponse.Page, searchRequest.Page);
            Assert.AreEqual(firstResponse.SearchString, searchRequest.SearchString);
            Assert.AreEqual(firstResponse.Skip, 0);

            searchRequest.NewSearch = false;
            searchRequest.Page = 2;

            var secondResponse = await searchService.GetBooksAsync(searchRequest);

            Assert.AreEqual(secondResponse.RecordsPerPage, searchRequest.RecordsPerPage);
            Assert.AreEqual(secondResponse.Page, searchRequest.Page);
            Assert.AreEqual(secondResponse.SearchString, searchRequest.SearchString);
            Assert.AreEqual(secondResponse.Skip, searchRequest.RecordsPerPage);
            Assert.AreEqual(secondResponse.RecordCount, firstResponse.RecordCount);



        }


        [Test()]
        public async Task GetBooks()
        {
            int bookId = 11;
            System.IO.FileInfo fi = new System.IO.FileInfo(_dbFilePath);
            Assert.IsTrue(fi.Exists);

            var bookService = new BookService(new BooksContext(_mockLogger.Object, "Data Source=" + _dbFilePath), _mapper);
            var book = await bookService.GetBookByIdAsync(bookId);

            Assert.IsNotNull(book);
            Assert.AreEqual(book.BookId, bookId);
            Assert.AreEqual(book.BookCategories.Count, 2);
        }


        [Test()]
        public async Task AddBook()
        {
            var context = new BooksContext(_mockLogger.Object, "Data Source=" + _dbFilePath);
            var searchService = new SearchService(context, _mapper, MockAppConfig.CreateMockAppConfig());
            var bookService = new BookService(context, _mapper);

            var searchRequest = new SearchRequest()
            {
                SearchString = "0440504708"
            };
            searchRequest.NewSearch = true;

            var response = await searchService.GetBooksAsync(searchRequest);
            if (response.Books.FirstOrDefault() != null)
            {
                context.Books.Remove(context.Books.Where(b => b.BookId == response.Books.FirstOrDefault().Id).First());
                context.SaveChanges();
            }
            response = null;


            var bookRequest = new BookRequest()
            {
                Title = "I'm Good Enough, I'm Smart Enough, and Doggone It, People Like Me!: Daily Affirmations By Stuart Smalley",
                ISBN = "0440504708",
                LongDescr = "Take a hilarious, healing journey with Stuart Smalley as he careens down the road to Recovery.  For one entire year Stuart recorded an affirmation a day...except when he had taken to his bed (but that's Okay)...and the result is the most entertaining and indispensable meditation book ever.",
                ShortDescr = "The ultimate meditation book, not to be grandiose...",
                PublishedDate = (new DateTime(1992, 10, 1)).ToString(),
                ThumbnailUrl = "https://images-na.ssl-images-amazon.com/images/I/51KXHWMIwYL._SX337_BO1,204,203,200_.jpg"
            };
            var addBookResponse = await bookService.AddBook(bookRequest);

            Assert.IsNotNull(addBookResponse);

        }

        [Test()]
        public async Task AddBookWithCategory()
        {
            var context = new BooksContext(_mockLogger.Object, "Data Source=" + _dbFilePath);
            var searchService = new SearchService(context, _mapper, MockAppConfig.CreateMockAppConfig());
            var bookService = new BookService(context, _mapper);

            var searchRequest = new SearchRequest()
            {
                SearchString = "044050470x"
            };
            searchRequest.NewSearch = true;

            var response = await searchService.GetBooksAsync(searchRequest);
            if (response.Books.FirstOrDefault() != null)
            {
                context.Books.Remove(context.Books.Where(b => b.BookId == response.Books.FirstOrDefault().Id).First());
            }
            response = null;


            var bookRequest = new BookRequest()
            {
                Title = "I'm Good Enough, I'm Smart Enough, and Doggone It, People Like Me!: Daily Affirmations By Stuart Smalley",
                ISBN = "044050470x",
                LongDescr = "Take a hilarious, healing journey with Stuart Smalley as he careens down the road to Recovery.  For one entire year Stuart recorded an affirmation a day...except when he had taken to his bed (but that's Okay)...and the result is the most entertaining and indispensable meditation book ever.",
                ShortDescr = "The ultimate meditation book, not to be grandiose...",
                PublishedDate = (new DateTime(1992, 10, 1)).ToString(),
                ThumbnailUrl = "https://images-na.ssl-images-amazon.com/images/I/51KXHWMIwYL._SX337_BO1,204,203,200_.jpg"
            };
            bookRequest.Categories = new int[] { 23 };
            var addBookResponse = await bookService.AddBook(bookRequest);

            Assert.IsNotNull(addBookResponse);

        }


    }
}

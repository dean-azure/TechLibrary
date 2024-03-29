﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechLibrary.Contracts.Requests;
using TechLibrary.Contracts.Responses;
using TechLibrary.Data;
using TechLibrary.Domain;
using TechLibrary.Interfaces;

namespace TechLibrary.Services
{

    public class BookService : IBookService
    {
        private readonly BooksContext _dataContext;
        private readonly IMapper _mapper;

        public BookService(BooksContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public Task<Book> GetBookByIdAsync(int bookid)
        {
            return (from b in _dataContext.Books
                .Include(bc => bc.BookCategories)
                .ThenInclude(c => c.Category)
                    where b.BookId == bookid
                    select b).FirstOrDefaultAsync();

        }

        public async Task<Book> AddBook(BookRequest newBook)
        {
            var book = _mapper.Map<Book>(newBook);
            if (newBook.Categories != null && newBook.Categories.Length > 0)
            {
                book.BookCategories = newBook.Categories.Select(catId => new BookCategory() { CategoryId = catId }).ToArray();
            }

            var result = await _dataContext.Books.AddAsync(book);
            await _dataContext.SaveChangesAsync();

            return await GetBookByIdAsync(result.Entity.BookId.Value);
        }

        public async Task<Book> EditBook(BookRequest editBook)
        {
            var book = await _dataContext.Books.Where(b => b.BookId == editBook.Id.Value).FirstOrDefaultAsync();
            book.BookId = editBook.Id;
            book.ISBN = editBook.ISBN;
            book.LongDescr = editBook.LongDescr;
            book.PublishedDate = editBook.PublishedDate;
            book.ShortDescr = editBook.ShortDescr;
            book.ThumbnailUrl = editBook.ThumbnailUrl;
            book.Title = editBook.Title;

            if (editBook?.Categories.Length > 0)
            {
                book.BookCategories = editBook?.Categories.Select(catId => new BookCategory() { CategoryId = catId, Book = book }).ToArray();
            }

            _dataContext.Books.Update(book);
            await _dataContext.SaveChangesAsync();

            return await GetBookByIdAsync(editBook.Id.Value);
        }

        public async Task DeleteBook(int bookId)
        {
            BookEditResponse editResponse = new BookEditResponse();

            var book = await GetBookByIdAsync(bookId);
            if (book != null)
            {
                book.Deleted = 1;
                var deleteResult = _dataContext.Books.Update(book);
                var deleteSaveResult = await _dataContext.SaveChangesAsync();
            }

        }


    }
}

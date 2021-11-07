using Microsoft.EntityFrameworkCore;
using Model.DTOs;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RepositoryPattern
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _appDbContext;
        public BookRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public List<GetBookDTO> GetBooks(PaginationDTO pagination)
        {
            var books = _appDbContext.Books.Include("Authors")
                                           .Include("Rates")
                                           .Skip((pagination.Page - 1) * pagination.Count)
                                           .Take(pagination.Count)
                                           .ToList();

            var bookDtoList = new List<GetBookDTO>();
            foreach (var book in books)
            {
                bookDtoList.Add(ExtractBookDTO(book));
            }

            return bookDtoList;
        }

        public GetBookDTO GetBook(int id)
        {
            var book = _appDbContext.Books.Include("Authors")
                                          .Include("Rates")
                                          .First(b => b.Id == id);
            return ExtractBookDTO(book);
        }

        public bool AddBook(AddBookDTO bookDto)
        {
            
            var newBook = new Book(bookDto.Title, bookDto.ReleaseDate);
            newBook.Authors = _appDbContext.Authors.Where(a => bookDto.AuthorIds.Contains(a.Id)).ToList();

            try
            {
                _appDbContext.Books.Add(newBook);
                _appDbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteBook(int id)
        {
            try
            {
                var book = _appDbContext.Books.Find(id);
                var bookRates = _appDbContext.BookRates.Where(br => br.FkBook == id);
                _appDbContext.BookRates.RemoveRange(bookRates);
                _appDbContext.Books.Remove(book);
                _appDbContext.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool RateBook(int id, short rate)
        {
            try
            {
                var book = _appDbContext.Books.Find(id);
                var newRate = new BookRate() { Type = RateType.BookRate, Book = book, Date = DateTime.Now, FkBook = id, Value = rate };
                _appDbContext.BookRates.Add(newRate);
                _appDbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private GetBookDTO ExtractBookDTO(Book book)
        {
            var authorsDtos = new List<AuthorInGetBookDTO>();
            var rateCount = book.Rates.Count();
            var averageRate = Math.Round(book.Rates.Average(b => b.Value), 1);

            book.Authors.ForEach(a => authorsDtos.Add(new AuthorInGetBookDTO(a.Id, a.FirstName, a.SecondName)));
            return new GetBookDTO(book.Id, book.Title, book.ReleaseDate, averageRate, rateCount, authorsDtos);
        }
                
    }
}

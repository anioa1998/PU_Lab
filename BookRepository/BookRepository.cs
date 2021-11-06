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
        public List<BookDTO> GetBooks()
        {
            var books = _appDbContext.Books.Include("Authors").Include("Rates").ToList();
            var bookDtoList = new List<BookDTO>();
            foreach (var book in books)
            {
                var averageRate = Convert.ToInt16(book.Rates.Average(b => b.Value));
                var authorsId = new List<int>();
                //book.Authors.ForEach(a => authors.Add(new AuthorInBookDTO() { Id = a.Id, FirstName = a.FirstName, SecondName = a.SecondName }));
                bookDtoList.Add(new BookDTO()
                {
                    Id = book.Id,
                    Title = book.Title,
                    AverageRate = averageRate,
                    RatesCount = book.Rates.Count,
                    ReleaseDate = book.ReleaseDate,
                    Authors = authorsId
                });
            }

            return bookDtoList;
        }

        public BookDTO GetBook(int id)
        {
            var book = _appDbContext.Books.Include("Authors").Include("Rates").Where(b => b.Id == id).FirstOrDefault();
            var averageRate = Convert.ToInt16(book.Rates.Average(b => b.Value));
            var authorsId = new List<int>();
           // book.Authors.ForEach(a => authorsId.Add(new AuthorInBookDTO() { Id = a.Id, FirstName = a.FirstName, SecondName = a.SecondName }));
            var bookDto = new BookDTO()
            {
                Id = book.Id,
                Title = book.Title,
                AverageRate = averageRate,
                RatesCount = book.Rates.Count,
                ReleaseDate = book.ReleaseDate,
                Authors = authorsId
            };
            return bookDto;
        }

        public void Dispose()
        {
            // throw new NotImplementedException();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Model.DTOs;
using Model.Models;
using RepositoryPattern.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RepositoryPattern
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMappingHelper _mappingHelper;
        private readonly IElasticHelper _elasticHelper;

        public BookRepository(AppDbContext appDbContext, IMappingHelper mappingHelper, IElasticHelper elasticHelper)
        {
            _appDbContext = appDbContext;
            _mappingHelper = mappingHelper;
            _elasticHelper = elasticHelper;
        }

        public List<GetBookDTO> GetBooks(PaginationDTO pagination)
        {
            return _elasticHelper.GetBookFromElastic(pagination: pagination)
                                 .ToList();
        }

        public List<GetBookDTO> SearchBooks(SearchBookDTO searchBook)
        {
            return _elasticHelper.GetBookFromElastic(searchBook: searchBook)
                                 .ToList();
                                 
        }

        public GetBookDTO GetBook(int id)
        {
            return _elasticHelper.GetBookFromElastic(id)
                                .Single();
        }

        public bool AddBook(AddBookDTO bookDto)
        {

            var newBook = new Book(bookDto.Title, bookDto.ReleaseDate, _mappingHelper.RandomString(1000));
            newBook.Authors = _appDbContext.Authors.Where(a => bookDto.AuthorIds.Contains(a.Id)).ToList();

            try
            {
                _appDbContext.Books.Add(newBook);
                _appDbContext.SaveChanges();

                var getBookDTO = new GetBookDTO(newBook.Id,
                                                newBook.Title,
                                                newBook.ReleaseDate,
                                                newBook.Description,
                                                0,
                                                0,
                                                newBook.Authors.Select(a => new AuthorInGetBookDTO(a.Id, a.FirstName, a.SecondName))
                                                               .ToList());
                _elasticHelper.AddBookToElastic(getBookDTO);


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
                _elasticHelper.DeleteBookFromElastic(id);
                return true;
            }
            catch (Exception ex)
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

                var rateCount = book.Rates.Count();
                var averageRate = book.Rates.Count > 0 ? Math.Round(book.Rates.Average(b => b.Value), 1) : 0;
                
                if (!_elasticHelper.UpdateBookRateInElastic(id, averageRate, rateCount))
                    return false;
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}

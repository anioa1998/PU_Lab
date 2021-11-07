using CQRS.BookFiles.Queries;
using CQRS.Helpers;
using Microsoft.EntityFrameworkCore;
using Model.DTOs;
using Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace CQRS.BookFiles.Handlers
{
    public class GetBooksQueryHandler : IQueryHandler<GetBooksQuery, List<GetBookDTO>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IBooksHelper _booksHelper;

        public GetBooksQueryHandler(AppDbContext appDbContext, IBooksHelper booksHelper)
        {
            _appDbContext = appDbContext;
            _booksHelper = booksHelper;
        }

        public List<GetBookDTO> Handle(GetBooksQuery query)
        {
            var books = _appDbContext.Books.Include("Authors")
                                                       .Include("Rates")
                                                       .Skip(query.Page * query.Count)
                                                       .Take(query.Count)
                                                       .ToList();

            var bookDtoList = new List<GetBookDTO>();
            foreach (var book in books)
            {
                bookDtoList.Add(_booksHelper.ExtractBookDTO(book));
            }

            return bookDtoList;
        }
    }
}

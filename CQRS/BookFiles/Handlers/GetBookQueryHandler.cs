using CQRS.BookFiles.Queries;
using CQRS.Helpers;
using Microsoft.EntityFrameworkCore;
using Model.DTOs;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.BookFiles.Handlers
{
    public class GetBookQueryHandler : IQueryHandler<GetBookQuery, GetBookDTO>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IBooksHelper _booksHelper;

        public GetBookQueryHandler(AppDbContext appDbContext, IBooksHelper booksHelper)
        {
            _appDbContext = appDbContext;
            _booksHelper = booksHelper;
        }

        public GetBookDTO Handle(GetBookQuery query)
        {
            var book = _appDbContext.Books.Include("Authors")
                                          .Include("Rates")
                                          .Single(b => b.Id == query.id);

            return _booksHelper.ExtractBookDTO(book);
        }
    }
}

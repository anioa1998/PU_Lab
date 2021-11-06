using Microsoft.EntityFrameworkCore;
using Model.DTOs;
using Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace CQRS
{
    public class GetBooksQueryHandler : IQueryHandler<GetBooksQuery, List<BookDTO>>
    {
        private readonly AppDbContext _appDbContext;

        public GetBooksQueryHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public List<BookDTO> Handle(GetBooksQuery query)
        {
            return _appDbContext.Books
                    .Include(b => b.Rates)
                    .Include(b => b.Authors)
                    .Skip(query.Page * query.Count)
                    .Take(query.Count)
                    .ToList()
                    .Select(b => new BookDTO
                    {
                        Id = b.Id,
                        ReleaseDate = b.ReleaseDate,
                        AverageRate = (short)(b.Rates.Count > 0 ? b.Rates.Average(r => r.Value) : 0),
                        RatesCount = b.Rates.Count,
                        Title = b.Title,
                        Authors = b.Authors.Select(a => new AuthorInBookDTO
                        {
                            FirstName = a.FirstName,
                            Id = a.Id,
                            SecondName = a.SecondName
                        }).ToList()
                    }).ToList();
        }
    }
}

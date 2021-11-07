using Model.DTOs;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Helpers
{
    public class AuthorsHelper : IAuthorsHelper
    {
        public GetAuthorDTO ExtractAuthorDTO(Author author)
        {
            var authorsDtos = new List<BookInGetAuthorDTO>();
            var rateCount = author.Rates.Count();
            var averageRate = Math.Round(author.Rates.Average(b => b.Value), 1);

            author.Books.ForEach(a => authorsDtos.Add(new BookInGetAuthorDTO(a.Id, a.Title)));
            return new GetAuthorDTO(author.Id, author.FirstName, author.SecondName, averageRate, rateCount, authorsDtos);
        }
    }
}

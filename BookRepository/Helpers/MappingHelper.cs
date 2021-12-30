using Model.DTOs;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern.Helpers
{
    public class MappingHelper :IMappingHelper
    {
        public GetBookDTO ExtractBookDTO(Book book)
        {
            var authorsDtos = new List<AuthorInGetBookDTO>();
            var rateCount = book.Rates.Count();
            var averageRate = book.Rates.Count > 0 ? Math.Round(book.Rates.Average(b => b.Value), 1) : 0;

            book.Authors.ForEach(a => authorsDtos.Add(new AuthorInGetBookDTO(a.Id, a.FirstName, a.SecondName)));
            return new GetBookDTO(book.Id, book.Title, book.ReleaseDate, book.Description, averageRate, rateCount, authorsDtos);
        }

        public GetAuthorDTO ExtractAuthorDTO(Author author)
        {
            var authorsDtos = new List<BookInGetAuthorDTO>();
            var rateCount = author.Rates.Count();
            var averageRate = author.Rates.Count > 0 ? Math.Round(author.Rates.Average(b => b.Value), 1) : 0;

            author.Books.ForEach(a => authorsDtos.Add(new BookInGetAuthorDTO(a.Id, a.Title)));
            return new GetAuthorDTO(author.Id, author.FirstName, author.SecondName, author.Cv, averageRate, rateCount, authorsDtos);
        }

        public string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}

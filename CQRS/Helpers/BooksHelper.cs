using Model.DTOs;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CQRS.Helpers
{
    public class BooksHelper : IBooksHelper
    {
        public GetBookDTO ExtractBookDTO(Book book)
        {
            var authorsDtos = new List<AuthorInGetBookDTO>();
            var rateCount = book.Rates.Count();
            var averageRate = Math.Round(book.Rates.Average(b => b.Value), 1);

            book.Authors.ForEach(a => authorsDtos.Add(new AuthorInGetBookDTO(a.Id, a.FirstName, a.SecondName)));
            return new GetBookDTO(book.Id, book.Title, book.ReleaseDate, averageRate, rateCount, authorsDtos);
        }
    }
}

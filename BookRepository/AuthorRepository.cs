﻿using Microsoft.EntityFrameworkCore;
using Model.DTOs;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly AppDbContext _appDbContext;

        public AuthorRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public bool AddAuthor(AddAuthorDTO authorDTO)
        {
            var newAuthor = new Author(authorDTO.FirstName, authorDTO.SecondName, RandomString(1000));

            try
            {
                _appDbContext.Authors.Add(newAuthor);
                _appDbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool AddRate(int id, short rate)
        {
            try
            {
                var author = _appDbContext.Authors.Find(id);
                var newRate = new AuthorRate() { Type = RateType.AuthorRate, Author = author, Date = DateTime.Now, FkAuthor = id, Value = rate };
                _appDbContext.AuthorRate.Add(newRate);
                _appDbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteAuthor(int id)
        {
            try
            {
                var author = _appDbContext.Authors.Find(id);
                var authorRates = _appDbContext.AuthorRate.Where(br => br.Id == id);
                _appDbContext.AuthorRate.RemoveRange(authorRates);
                _appDbContext.Authors.Remove(author);
                _appDbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<GetAuthorDTO> GetAuthors(PaginationDTO pagination)
        {
            var authors = _appDbContext.Authors.Include("Books")
                                               .Include("Rates")
                                               .Skip(pagination.Page * pagination.Count)
                                               .Take(pagination.Count)
                                               .ToList();

            var authorDtoList = new List<GetAuthorDTO>();
            foreach (var author in authors)
            {
                authorDtoList.Add(ExtractAuthorDTO(author));
            }

            return authorDtoList;
        }

        private GetAuthorDTO ExtractAuthorDTO(Author author)
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

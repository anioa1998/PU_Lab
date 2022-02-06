using Microsoft.EntityFrameworkCore;
using Model.DTOs;
using Model.Models;
using RepositoryPattern.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RepositoryPattern
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMappingHelper _mappingHelper;
        private readonly IElasticHelper _elasticHelper;

        public AuthorRepository(AppDbContext appDbContext, IMappingHelper mappingHelper, IElasticHelper elasticHelper)
        {
            _appDbContext = appDbContext;
            _mappingHelper = mappingHelper;
            _elasticHelper = elasticHelper;
        }

        public bool AddAuthor(AddAuthorDTO authorDTO)
        {
            var newAuthor = new Author(authorDTO.FirstName, authorDTO.SecondName, _mappingHelper.RandomString(1000));

            try
            {
                _appDbContext.Authors.Add(newAuthor);
                _appDbContext.SaveChanges();
                _elasticHelper.AddAuthorToElastic(_mappingHelper.ExtractAuthorDTO(newAuthor));
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
                var author = _appDbContext.Authors.Include("Rates")
                                                  .Single(a => a.Id == id);
                var newRate = new AuthorRate() { Type = RateType.AuthorRate, Author = author, Date = DateTime.Now, FkAuthor = id, Value = rate };
                _appDbContext.AuthorRate.Add(newRate);
                _appDbContext.SaveChanges();

                var rateCount = author.Rates.Count();
                var averageRate = author.Rates.Count > 0 ? Math.Round(author.Rates.Average(b => b.Value), 1) : 0;

                if (!_elasticHelper.UpdateAuthorRateInElastic(id, averageRate, rateCount))
                    return false;

                return true;
            }
            catch
            {
                return false;
            }
        }

        public int CountAuthors()
        {
            return _appDbContext.Authors.Count();
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
                _elasticHelper.DeleteAuthorFromElastic(id);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public GetAuthorDTO GetAuthor(int id)
        {
            return _elasticHelper.GetAuthorFromElastic(id)
                                 .Single();
        }

        public List<GetAuthorDTO> GetAuthors(PaginationDTO pagination)
        {
            return _elasticHelper.GetAuthorFromElastic(pagination: pagination).ToList();
        }
        public List<GetAuthorDTO> SearchAuthors(SearchAuthorDTO searchAuthor)
        {
            return _elasticHelper.GetAuthorFromElastic(searchAuthor: searchAuthor).ToList();
        }

        public bool UpdateAuthor(GetAuthorDTO authorDTO)
        {
            try
            {
                var authorModel = _appDbContext.Authors.Find(authorDTO.Id);

                authorModel.FirstName = authorDTO.FirstName;
                authorModel.SecondName = authorDTO.SecondName;
                authorModel.Books = _appDbContext.Books.Where(a => authorDTO.Books.Exists(g => g.Id == a.Id)).ToList();

                _appDbContext.Authors.Update(authorModel);
                _appDbContext.SaveChanges();
                _elasticHelper.UpdateAuthorInElastic(authorDTO);

                foreach (var bookModel in authorModel.Books)
                {
                    var bookDTO = _mappingHelper.ExtractBookDTO(bookModel);
                    _elasticHelper.UpdateBookInElastic(bookDTO);
                }
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}

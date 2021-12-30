using Microsoft.EntityFrameworkCore;
using Model.DTOs;
using Model.Models;
using RepositoryPattern.Helpers;
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
            return _elasticHelper.GetAuthor(pagination: pagination).ToList();
        }

  
    }
}

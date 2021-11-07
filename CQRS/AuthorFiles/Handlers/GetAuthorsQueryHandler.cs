using CQRS.AuthorFiles.Queries;
using CQRS.Helpers;
using Microsoft.EntityFrameworkCore;
using Model.DTOs;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.AuthorFiles.Handlers
{
    public class GetAuthorsQueryHandler : IQueryHandler<GetAuthorsQuery, List<GetAuthorDTO>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IAuthorsHelper _authorsHelper;

        public GetAuthorsQueryHandler(AppDbContext appDbContext, IAuthorsHelper authorsHelper)
        {
            _appDbContext = appDbContext;
            _authorsHelper = authorsHelper;
        }

        public List<GetAuthorDTO> Handle(GetAuthorsQuery query)
        {
            var authors = _appDbContext.Authors.Include("Books")
                                               .Include("Rates")
                                               .Skip(query.Page * query.Count)
                                               .Take(query.Count)
                                               .ToList();

            var authorDtoList = new List<GetAuthorDTO>();
            foreach (var author in authors)
            {
                authorDtoList.Add(_authorsHelper.ExtractAuthorDTO(author));
            }

            return authorDtoList;
        }
    }
}

using CQRS.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Model.DTOs;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MediatRProject.AuthorFiles.Queries
{
    public record GetMediatRAuthorsQuery(PaginationDTO paginationDTO) : IRequest<List<GetAuthorDTO>>;

    public class GetMediatRAuthorsQueryHandler : IRequestHandler<GetMediatRAuthorsQuery, List<GetAuthorDTO>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IAuthorsHelper _authorsHelper;

        public GetMediatRAuthorsQueryHandler(IAuthorsHelper authorsHelper, AppDbContext appDbContext)
        {
            _authorsHelper = authorsHelper;
            _appDbContext = appDbContext;
        }

        public Task<List<GetAuthorDTO>> Handle(GetMediatRAuthorsQuery request, CancellationToken cancellationToken)
        { 
            var authors = _appDbContext.Authors.Include("Books")
                                               .Include("Rates")
                                               .Skip(request.paginationDTO.Page * request.paginationDTO.Count)
                                               .Take(request.paginationDTO.Count)
                                               .ToList();

            var authorDtoList = new List<GetAuthorDTO>();
            foreach (var author in authors)
            {
                authorDtoList.Add(_authorsHelper.ExtractAuthorDTO(author));
            }

            return Task.FromResult(authorDtoList);
        }
    }
}

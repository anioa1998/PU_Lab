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

namespace MediatRProject.BookFiles.Queries
{
    public record GetMediatRBookQuery(int id) : IRequest<GetBookDTO>;

    public class GetMediatRBookQueryHandler : IRequestHandler<GetMediatRBookQuery, GetBookDTO>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IBooksHelper _booksHelper;

        public Task<GetBookDTO> Handle(GetMediatRBookQuery request, CancellationToken cancellationToken)
        {
            var book = _appDbContext.Books.Include("Authors")
                                         .Include("Rates")
                                         .Single(b => b.Id == request.id);

            return Task.FromResult(_booksHelper.ExtractBookDTO(book));
        }
    }
}

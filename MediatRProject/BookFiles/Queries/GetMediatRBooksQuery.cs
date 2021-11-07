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
    public record GetMediatRBooksQuery(PaginationDTO paginationDTO) : IRequest<List<GetBookDTO>>;

    public class GetMediatRBooksQueryHandler : IRequestHandler<GetMediatRBooksQuery, List<GetBookDTO>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IBooksHelper _booksHelper;

        public Task<List<GetBookDTO>> Handle(GetMediatRBooksQuery request, CancellationToken cancellationToken)
        { 
            var books = _appDbContext.Books.Include("Authors")
                                                        .Include("Rates")
                                                        .Skip(request.paginationDTO.Page * request.paginationDTO.Count)
                                                        .Take(request.paginationDTO.Count)
                                                        .ToList();

            var bookDtoList = new List<GetBookDTO>();
            foreach (var book in books)
            {
                bookDtoList.Add(_booksHelper.ExtractBookDTO(book));
            }

            return Task.FromResult(bookDtoList);
        }
    }
}

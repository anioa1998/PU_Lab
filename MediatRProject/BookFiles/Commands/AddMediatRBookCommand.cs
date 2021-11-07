using MediatR;
using Model.DTOs;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MediatRProject.BookFiles.Commands
{
    public record AddMediatRBookCommand(AddBookDTO bookDTO) : IRequest<bool>;

    public class AddMediatRBookCommandHandler : IRequestHandler<AddMediatRBookCommand, bool>
    {
        private readonly AppDbContext _appDbContext;

        public Task<bool> Handle(AddMediatRBookCommand request, CancellationToken cancellationToken)
        {
            Book book = new Book(request.bookDTO.Title, request.bookDTO.ReleaseDate);
            book.Authors = _appDbContext.Authors.Where(a => request.bookDTO.AuthorIds.Contains(a.Id)).ToList();
            _appDbContext.Books.Add(book);
            _appDbContext.SaveChanges();

            return Task.FromResult(true);
        }
    }
}

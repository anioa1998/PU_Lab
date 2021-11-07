using MediatR;
using Model.DTOs;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MediatRProject.AuthorFiles.Commands
{
    public record AddMediatRAuthorCommand(AddAuthorDTO authorDTO) : IRequest<bool>;

    public class AddMediatRAuthorCommandHandler : IRequestHandler<AddMediatRAuthorCommand, bool>
    {
        private readonly AppDbContext _appDbContext;


        public Task<bool> Handle(AddMediatRAuthorCommand request, CancellationToken cancellationToken)
        {
            var newAuthor = new Author(request.authorDTO.FirstName, request.authorDTO.SecondName);

            _appDbContext.Authors.Add(newAuthor);
            _appDbContext.SaveChanges();
            return Task.FromResult(true);
        }
    }
}

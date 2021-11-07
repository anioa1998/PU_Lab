using MediatR;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MediatRProject.AuthorFiles.Commands
{
    public record DeleteMediatRAuthorCommand(int id) : IRequest<bool>;

    public class DeleteMediatRAuthorCommandHandler : IRequestHandler<DeleteMediatRAuthorCommand, bool>
    {
        private readonly AppDbContext _appDbContext;

        public Task<bool> Handle(DeleteMediatRAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = _appDbContext.Authors.Find(request.id);
            var authorRates = _appDbContext.AuthorRate.Where(br => br.Id == request.id);
            _appDbContext.AuthorRate.RemoveRange(authorRates);
            _appDbContext.Authors.Remove(author);
            _appDbContext.SaveChanges();
            return Task.FromResult(true);
        }
    }
}

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
    public record AddMediatRAuthorRateCommand(int id, short rate) : IRequest<bool>;

    public class AddMediatRAuthorRateCommandHandler : IRequestHandler<AddMediatRAuthorRateCommand, bool>
    {
        private readonly AppDbContext _appDbContext;

        public Task<bool> Handle(AddMediatRAuthorRateCommand request, CancellationToken cancellationToken)
        {
            var author = _appDbContext.Authors.Find(request.id);
            var newRate = new AuthorRate() { Type = RateType.AuthorRate, Author = author, Date = DateTime.Now, FkAuthor = request.id, Value = request.rate };
            _appDbContext.AuthorRate.Add(newRate);
            _appDbContext.SaveChanges();
            return Task.FromResult(true);
        }
    }
}

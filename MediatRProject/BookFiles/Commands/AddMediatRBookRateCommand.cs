using MediatR;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MediatRProject.BookFiles.Commands
{
    public record AddMediatRBookRateCommand(int id, short rate) : IRequest<bool>;

    public class AddMediatRBookRateCommandHandler : IRequestHandler<AddMediatRBookRateCommand, bool>
    {
        private readonly AppDbContext _appDbContext;

        public Task<bool> Handle(AddMediatRBookRateCommand request, CancellationToken cancellationToken)
        {
            var book = _appDbContext.Books.Find(request.id);
            var newRate = new BookRate() { Type = RateType.BookRate, Book = book, Date = DateTime.Now, FkBook = request.id, Value = request.rate };
            _appDbContext.BookRates.Add(newRate);
            _appDbContext.SaveChanges();
            return Task.FromResult(true);
        }
    }
}

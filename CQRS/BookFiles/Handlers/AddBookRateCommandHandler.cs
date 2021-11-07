using CQRS.BookFiles.Commands;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.BookFiles.Handlers
{
    public class AddBookRateCommandHandler : ICommandHandler<AddBookRateCommand>
    {
        private readonly AppDbContext _appDbContext;

        public AddBookRateCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void Handle(AddBookRateCommand command)
        {
            var book = _appDbContext.Books.Find(command.id);
            var newRate = new BookRate() { Type = RateType.BookRate, Book = book, Date = DateTime.Now, FkBook = command.id, Value = command.rate };
            _appDbContext.BookRates.Add(newRate);
            _appDbContext.SaveChanges();
        }
    }
}

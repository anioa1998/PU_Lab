using CQRS.AuthorFiles.Commands;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.AuthorFiles.Handlers
{
    public class AddAuthorRateCommandHandler : ICommandHandler<AddAuthorRateCommand>
    {
        private readonly AppDbContext _appDbContext;

        public AddAuthorRateCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void Handle(AddAuthorRateCommand command)
        {
            var author = _appDbContext.Authors.Find(command.id);
            var newRate = new AuthorRate() { Type = RateType.AuthorRate, Author = author, Date = DateTime.Now, FkAuthor = command.id, Value = command.rate };
            _appDbContext.AuthorRate.Add(newRate);
            _appDbContext.SaveChanges();
        }
    }
}

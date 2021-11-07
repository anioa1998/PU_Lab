using CQRS.AuthorFiles.Commands;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.AuthorFiles.Handlers
{
    public class DeleteAuthorCommandHandler : ICommandHandler<DeleteAuthorCommand>
    {
        private readonly AppDbContext _appDbContext;

        public DeleteAuthorCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void Handle(DeleteAuthorCommand command)
        {
            var author = _appDbContext.Authors.Find(command.id);
            var authorRates = _appDbContext.AuthorRate.Where(br => br.Id == command.id);
            _appDbContext.AuthorRate.RemoveRange(authorRates);
            _appDbContext.Authors.Remove(author);
            _appDbContext.SaveChanges();
        }
    }
}

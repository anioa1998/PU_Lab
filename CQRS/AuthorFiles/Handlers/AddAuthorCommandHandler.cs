using CQRS.AuthorFiles.Commands;
using Model.Models;

namespace CQRS.AuthorFiles.Handlers
{
    public class AddAuthorCommandHandler : ICommandHandler<AddAuthorCommand>
    {
        private readonly AppDbContext _appDbContext;

        public AddAuthorCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void Handle(AddAuthorCommand command)
        {
            var newAuthor = new Author(command.authorDTO.FirstName, command.authorDTO.SecondName);

            _appDbContext.Authors.Add(newAuthor);
            _appDbContext.SaveChanges();

        }
    }
}

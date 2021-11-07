using CQRS.BookFiles.Commands;
using Model.Models;

namespace CQRS.BookFiles.Handlers
{
    public class DeleteBookCommandHandler : ICommandHandler<DeleteBookCommand>
    {
        private readonly AppDbContext _appDbContext;

        public DeleteBookCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void Handle(DeleteBookCommand command)
        {
            Book book = _appDbContext.Books.Find(command.Id);
            _appDbContext.Books.Remove(book);
            _appDbContext.SaveChanges();
        }
    }
}

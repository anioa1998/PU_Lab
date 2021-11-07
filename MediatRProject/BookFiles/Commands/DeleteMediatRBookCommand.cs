using MediatR;
using Model.Models;
using System.Threading;
using System.Threading.Tasks;

namespace MediatRProject.BookFiles.Commands
{
    public record DeleteMediatRBookCommand(int id) : IRequest<bool>;

    public class DeleteMediatRBookCommandHandler : IRequestHandler<DeleteMediatRBookCommand, bool>
    {
        private readonly AppDbContext _appDbContext;

        public Task<bool> Handle(DeleteMediatRBookCommand request, CancellationToken cancellationToken)
        {
            Book book = _appDbContext.Books.Find(request.id);
            _appDbContext.Books.Remove(book);
            _appDbContext.SaveChanges();
            return Task.FromResult(true);
        }
    }
}

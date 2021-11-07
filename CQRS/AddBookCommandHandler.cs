using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS
{
    public class AddBookCommandHandler : ICommandHandler<AddBookCommand>
    {
        private readonly AppDbContext _appDbContext;

        public AddBookCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void Handle(AddBookCommand command)
        {
            //Book book = new Book
            //{
            //    Title = command.Title,
            //    ReleaseDate = command.ReleaseDate
            //};

            //book.Authors = _appDbContext.Authors.Where(a => command.AuthorsId.Contains(a.Id)).ToList();
            //_appDbContext.Books.Add(book);
            //_appDbContext.SaveChanges();
        }
    }
}

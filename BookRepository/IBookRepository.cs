using Model.DTOs;
using System;
using System.Collections.Generic;

namespace RepositoryPattern
{
    public interface IBookRepository : IDisposable
    {
        List<BookDTO> GetBooks();
        BookDTO GetBook(int id);
    }
}

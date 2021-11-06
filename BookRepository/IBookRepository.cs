using Model.DTOs;
using System;
using System.Collections.Generic;

namespace RepositoryPattern
{
    public interface IBookRepository : IDisposable
    {
        List<BookDTO> GetBooks();
        BookDTO GetBook(int id);
        bool AddBook(BookDTO bookDto);
        bool DeleteBook(int id);
        bool RateBook(int id, int rate);
    }
}

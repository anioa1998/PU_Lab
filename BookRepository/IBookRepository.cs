﻿using Model.DTOs;
using System;
using System.Collections.Generic;

namespace RepositoryPattern
{
    public interface IBookRepository
    {
        List<GetBookDTO> GetBooks(PaginationDTO pagination);
        GetBookDTO GetBook(int id);
        bool AddBook(AddBookDTO bookDto);
        bool DeleteBook(int id);
        bool RateBook(int id, short rate);
    }
}

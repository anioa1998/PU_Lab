using Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepositoryPattern.Helpers
{
    public interface IElasticHelper
    {
        void CreateIndex();
        GetBookDTO AddBookToElastic(GetBookDTO book);
        GetAuthorDTO AddAuthorToElastic(GetAuthorDTO author);
        IEnumerable<GetBookDTO> GetBook(int id = 0, PaginationDTO pagination = null);
        IEnumerable<GetAuthorDTO> GetAuthor(int id = 0, PaginationDTO pagination= null);
    }
}

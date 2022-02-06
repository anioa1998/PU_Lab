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
        IEnumerable<GetBookDTO> GetBookFromElastic(int id = 0, SearchBookDTO searchBook = null, PaginationDTO pagination = null);
        IEnumerable<GetAuthorDTO> GetAuthorFromElastic(int id = 0, SearchAuthorDTO searchAuthor = null, PaginationDTO pagination = null);
        bool DeleteBookFromElastic(int id);
        bool DeleteAuthorFromElastic(int id);
        bool UpdateBookRateInElastic(int id, double averageRate, int count);
        bool UpdateAuthorRateInElastic(int id, double averageRate, int count);
        bool UpdateBookInElastic(GetBookDTO bookToUpdate);
        bool UpdateAuthorInElastic(GetAuthorDTO authorToUpdate);
    }
}

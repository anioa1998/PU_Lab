using Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern
{
    public interface IAuthorRepository
    {
        public List<GetAuthorDTO> GetAuthors(PaginationDTO pagination);
        public List<GetAuthorDTO> SearchAuthors(SearchAuthorDTO searchAuthor);
        public GetAuthorDTO GetAuthor(int id);
        public bool AddAuthor(AddAuthorDTO authorDTO);
        public bool DeleteAuthor(int id);
        public bool AddRate(int id, short rate);
    }
}

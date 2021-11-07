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
        public List<GetAuthorDTO> GetAuthors();
        public bool AddAuthor(GetAuthorDTO authorDTO);
        public bool DeleteAuthor(int id);
        public bool AddRate(int id, int rate);
    }
}

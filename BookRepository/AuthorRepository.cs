using Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern
{
    public class AuthorRepository : IAuthorRepository
    {
        public bool AddAuthor(GetAuthorDTO authorDTO)
        {
            throw new NotImplementedException();
        }

        public bool AddRate(int id, int rate)
        {
            throw new NotImplementedException();
        }

        public bool DeleteAuthor(int id)
        {
            throw new NotImplementedException();
        }

        public List<GetAuthorDTO> GetAuthors()
        {
            throw new NotImplementedException();
        }
    }
}

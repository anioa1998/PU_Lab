using Model.DTOs;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern.Helpers
{
    public interface IMappingHelper
    {
        GetAuthorDTO ExtractAuthorDTO(Author author);
        GetBookDTO ExtractBookDTO(Book book);
        string RandomString(int length);
    }
}

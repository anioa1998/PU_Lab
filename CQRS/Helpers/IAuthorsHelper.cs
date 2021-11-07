using Model.DTOs;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Helpers
{
    public interface IAuthorsHelper
    {
        GetAuthorDTO ExtractAuthorDTO(Author author);
    }
}

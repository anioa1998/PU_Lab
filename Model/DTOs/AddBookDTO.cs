using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs
{
    public record AddBookDTO(string Title, DateTime ReleaseDate, List<int> AuthorIds);
}

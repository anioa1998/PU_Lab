using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs
{
    public record GetAuthorDTO(int Id, string FirstName, string SecondName, double AverageRate, int RatesCount, List<BookInGetAuthorDTO> Books);
   
}

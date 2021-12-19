using Nest;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs
{
    [ElasticsearchType(IdProperty = nameof(Id))]
    public record GetAuthorDTO(int Id,
                               string FirstName,
                               string SecondName,
                               [MaxLength(1000)] string CV,
                               double AverageRate,
                               int RatesCount,
                               List<BookInGetAuthorDTO> Books);
   
}

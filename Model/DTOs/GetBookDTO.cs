using Nest;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Model.DTOs
{
    [ElasticsearchType(IdProperty = nameof(Id))]
    public record GetBookDTO(int Id,
                             string Title,
                             DateTime ReleaseDate,
                             [MaxLength(1000)] string Description,
                             double AverageRate,
                             int RatesCount,
                             List<AuthorInGetBookDTO> Authors);
    
}

using System;
using System.Collections.Generic;

namespace Model.DTOs
{
    public record GetBookDTO(int Id, string Title, DateTime ReleaseDate, double AverageRate, int RatesCount, List<AuthorInGetBookDTO> Authors);
    
}

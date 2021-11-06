using System;
using System.Collections.Generic;

namespace Model.DTOs
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public short AverageRate { get; set; }
        public int RatesCount { get; set; }
        public List<AuthorInBookDTO> Authors { get; set; }
    }
}

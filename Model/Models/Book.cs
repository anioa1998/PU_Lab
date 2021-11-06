using System;
using System.Collections.Generic;

namespace Model.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public List<Author> Authors { get; set; }
        public List<BookRate> Rates { get; set; }
    }
}

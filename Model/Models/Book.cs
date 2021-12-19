using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Model.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
        public List<Author> Authors { get; set; }
        public List<BookRate> Rates { get; set; }

        public Book(string title, DateTime releaseDate, string description)
        {
            Title = title;
            ReleaseDate = releaseDate;
            Description = description;
        }
    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Model.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        [MaxLength(1000)]
        public string Cv { get; set; }
        public List<Book> Books { get; set; }
        public List<AuthorRate> Rates { get; set; }

        public Author(string firstName, string secondName, string cv)
        {
            FirstName = firstName;
            SecondName = secondName;
            Cv = cv;
        }
    }
}

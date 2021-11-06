using System.Collections.Generic;

namespace Model.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public List<Book> Books { get; set; }
        public List<AuthorRate> Rates { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs
{
    public class AuthorInGetBookDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }

        public AuthorInGetBookDTO(int id, string firstName, string secondName)
        {
            Id = id;
            FirstName = firstName;
            SecondName = secondName;
        }

        public override string ToString()
        {
            return $"{FirstName} {SecondName}";
        }
    }

}

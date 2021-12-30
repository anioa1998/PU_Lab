using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs
{
    public class SearchBookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
    }
}

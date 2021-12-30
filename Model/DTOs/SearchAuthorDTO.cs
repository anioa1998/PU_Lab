using System.ComponentModel.DataAnnotations;

namespace Model.DTOs
{
    public class SearchAuthorDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        [MaxLength(1000)]
        public string CV { get; set; }
        public string Title { get; set; }
    }
}

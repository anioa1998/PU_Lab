using System.ComponentModel.DataAnnotations;

namespace Model.DTOs
{
    public class SearchAuthorDTO
    {
        public int Id { get; set; }
        public bool MatchAll { get; set; }
        public string MasterQuery { get; set; }
    }
}

namespace Model.DTOs
{
    public class BookInGetAuthorDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public BookInGetAuthorDTO(int id, string title)
        {
            Id = id;
            Title = title;
        }

        public override string ToString()
        {
            return $"{Title}";
        }
    }
}

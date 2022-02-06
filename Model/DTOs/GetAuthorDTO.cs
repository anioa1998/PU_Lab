using Nest;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Model.DTOs
{
    [ElasticsearchType(IdProperty = nameof(Id))]
    public class GetAuthorDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        [MaxLength(1000)]
        public string CV { get; set; }
        public double AverageRate { get; set; }
        public int RatesCount { get; set; }
        public List<BookInGetAuthorDTO> Books { get; set; }

        public GetAuthorDTO() { }
        public GetAuthorDTO(int id, double averageRate, int ratesCount)
        {
            Id = id;
            AverageRate = averageRate;
            RatesCount = ratesCount;
        }

        public GetAuthorDTO(int id, string firstName, string secondName, string cV, double averageRate, int ratesCount, List<BookInGetAuthorDTO> books)
        : this(id, averageRate, ratesCount)
        {
            FirstName = firstName;
            SecondName = secondName;
            CV = cV;
            Books = books;
        }
    }

}

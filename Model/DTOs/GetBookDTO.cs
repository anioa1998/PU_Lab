using Nest;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Model.DTOs
{
    [ElasticsearchType(IdProperty = nameof(Id))]
    public class GetBookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
        public double AverageRate { get; set; }
        public int RatesCount { get; set; }
        public List<AuthorInGetBookDTO> Authors { get; set; }

        public GetBookDTO() { }
        public GetBookDTO(int id, double averageRate, int ratesCount)
        {
            Id = id;
            AverageRate = averageRate;
            RatesCount = ratesCount;
        }

        public GetBookDTO(int id, string title, DateTime releaseDate, string description, double averageRate, int ratesCount, List<AuthorInGetBookDTO> authors)
        : this(id, averageRate, ratesCount)
        {
            Title = title;
            ReleaseDate = releaseDate;
            Description = description;
            Authors = authors;
        }
    }

}

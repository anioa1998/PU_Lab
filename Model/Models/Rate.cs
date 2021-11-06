using System;

namespace Model.Models
{
    public abstract class Rate
    {
        public int Id { get; set; }
        public short Value { get; set; }
        public DateTime Date { get; set; }
        public RateType Type { get; set; }
    }
    public enum RateType
    {
        AuthorRate, BookRate
    }
}

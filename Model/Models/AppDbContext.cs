using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<AuthorRate> AuthorRate { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookRate> BookRates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Rate>()
                .HasDiscriminator(x => x.Type)
                .HasValue<AuthorRate>(RateType.AuthorRate)
                .HasValue<BookRate>(RateType.BookRate);
        }
    }
}


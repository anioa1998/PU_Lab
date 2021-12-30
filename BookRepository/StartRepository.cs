using Microsoft.EntityFrameworkCore;
using Model.Models;
using RepositoryPattern.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern
{
    public class StartRepository : IStartRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IElasticHelper _elasticHelper;
        private readonly IMappingHelper _mappingHelper;

        public StartRepository(AppDbContext appDbContext, IElasticHelper elasticHelper, IMappingHelper mappingHelper)
        {
            _appDbContext = appDbContext;
            _elasticHelper = elasticHelper;
            _mappingHelper = mappingHelper;
        }

        public void StartupCreateIndex()
        {
            _elasticHelper.CreateIndex();
            CleanUpDatabase();
            GenerateAuthors();
            GenerateBooks();
            AddAuthorsToElastic();
            AddBooksToElastic();
        }

        private void CleanUpDatabase()
        {
            _appDbContext.Books.RemoveRange(_appDbContext.Books);
            _appDbContext.BookRates.RemoveRange(_appDbContext.BookRates);
            _appDbContext.Authors.RemoveRange(_appDbContext.Authors);
            _appDbContext.AuthorRate.RemoveRange(_appDbContext.AuthorRate);

            _appDbContext.SaveChanges();
        }

        private void GenerateAuthors()
        {
            var authors = new List<Author>();
            var authorRates = new List<AuthorRate>();

            var random = new Random();

            for (int i = 0; i < 10; i++)
            {
                authors.Add(new Author(GenerateAuthorName(), GenerateAuthorSurname(), _mappingHelper.RandomString(1000)));
            }

            _appDbContext.Authors.AddRange(authors);
            _appDbContext.SaveChanges();

            foreach (var author in authors)
            {
                for (int j = 0; j < random.Next(1, 5); j++)
                {
                    authorRates.Add(new AuthorRate() { Type = RateType.AuthorRate, Author = author, Date = DateTime.Now, FkAuthor = author.Id, Value = (short)random.Next(1, 5) });
                }
            }

            _appDbContext.AuthorRate.AddRange(authorRates);
            _appDbContext.SaveChanges();
        }

        private void AddAuthorsToElastic()
        {
            var authors = _appDbContext.Authors.Include("Books")
                                               .Include("Rates")
                                               .ToList();

            foreach (var author in authors)
            {
                _elasticHelper.AddAuthorToElastic(_mappingHelper.ExtractAuthorDTO(author));
            }
        }

        private void AddBooksToElastic()
        {
            var books = _appDbContext.Books.Include("Authors")
                                            .Include("Rates")
                                            .ToList();

            foreach (var book in books)
            {
                _elasticHelper.AddBookToElastic(_mappingHelper.ExtractBookDTO(book));
            }
        }

        private void GenerateBooks()
        {
            var random = new Random();

            var titles = new List<string> { "Harry Potter i Kamień Filozoficzny",
                                            "Harry Potter i Czara Ognia",
                                            "Mindf*ck. Cambridge Analytica, czyli jak popsuć demokrację.",
                                            "Mali Bogowie",
                                            "Mali Bogowie 2",
                                            "Jedyny samolot na niebie",
                                            "27 śmierci Toby'ego Obeda",
                                            "Wiedźmin: Krew Elfów",
                                            "Sekretne życie mózgu nastolatka",
                                            "Przygody Robinsona Crusoe"};

            var books = new List<Book>();
            var bookRates = new List<BookRate>();
            var authors = _appDbContext.Authors.ToList();

            foreach (var title in titles)
            {
                var book = new Book(title, DateTime.Now, _mappingHelper.RandomString(1000));
                book.Authors = new List<Author>();
                for (int i = 0; i < random.Next(1, 5); i++)
                {
                    book.Authors.Add(authors[random.Next(0, authors.Count() - 1)]);
                }
                books.Add(book);
            }

            _appDbContext.Books.AddRange(books);
            _appDbContext.SaveChanges();

            foreach (var book in books)
            {
                for (int j = 0; j < random.Next(1, 5); j++)
                {
                    bookRates.Add(new BookRate() { Type = RateType.BookRate, Book = book, Date = DateTime.Now, FkBook = book.Id, Value = (short)random.Next(1, 5) });
                }
            }

            _appDbContext.BookRates.AddRange(bookRates);
            _appDbContext.SaveChanges();
        }
        private string GenerateAuthorName()
        {
            var random = new Random();
            var names = new List<string>() {"Michael","Christopher","Jessica","Matthew","Ashley","Jennifer",
                                            "Joshua","Amanda","Daniel","David","James","Robert","John","Joseph",
                                            "Andrew","Ryan","Brandon","Jason","Justin","Sarah"};
            return names[random.Next(0, names.Count - 1)];

        }

        private string GenerateAuthorSurname()
        {
            var random = new Random();
            var surnames = new List<string> { "Smith","Johnson","Williams","Brown","Jones","Miller","Davis",
                                              "Garcia","Rodriguez","Wilson","Martinez","Anderson","Taylor",
                                              "Thomas","Hernandez","Moore"};
            return surnames[random.Next(0, surnames.Count - 1)];
        }

        
    }
}

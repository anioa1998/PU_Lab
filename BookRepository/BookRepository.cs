using Microsoft.EntityFrameworkCore;
using Model.DTOs;
using Model.Models;
using RepositoryPattern.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RepositoryPattern
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IElasticHelper _elasticHelper;
        public BookRepository(AppDbContext appDbContext, IElasticHelper elasticHelper)
        {
            _appDbContext = appDbContext;
            _elasticHelper = elasticHelper;
        }
        public List<GetBookDTO> GetBooks(PaginationDTO pagination)
        {
            var books = _appDbContext.Books.Include("Authors")
                                           .Include("Rates")
                                           .Skip((pagination.Page) * pagination.Count)
                                           .Take(pagination.Count)
                                           .ToList();

            var bookDtoList = new List<GetBookDTO>();
            foreach (var book in books)
            {
                bookDtoList.Add(ExtractBookDTO(book));
            }

            return bookDtoList;
        }

        public GetBookDTO GetBook(int id)
        {
            var book = _appDbContext.Books.Include("Authors")
                                          .Include("Rates")
                                          .First(b => b.Id == id);
            return ExtractBookDTO(book);
        }

        public bool AddBook(AddBookDTO bookDto)
        {

            var newBook = new Book(bookDto.Title, bookDto.ReleaseDate, RandomString(1000));
            newBook.Authors = _appDbContext.Authors.Where(a => bookDto.AuthorIds.Contains(a.Id)).ToList();

            try
            {
                _appDbContext.Books.Add(newBook);
                _appDbContext.SaveChanges();

                var getBookDTO = new GetBookDTO(newBook.Id,
                                                newBook.Title,
                                                newBook.ReleaseDate,
                                                newBook.Description,
                                                0,
                                                0,
                                                newBook.Authors.Select(a => new AuthorInGetBookDTO(a.Id, a.FirstName, a.SecondName))
                                                               .ToList());



                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteBook(int id)
        {
            try
            {
                var book = _appDbContext.Books.Find(id);
                var bookRates = _appDbContext.BookRates.Where(br => br.FkBook == id);
                _appDbContext.BookRates.RemoveRange(bookRates);
                _appDbContext.Books.Remove(book);
                _appDbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool RateBook(int id, short rate)
        {
            try
            {
                var book = _appDbContext.Books.Find(id);
                var newRate = new BookRate() { Type = RateType.BookRate, Book = book, Date = DateTime.Now, FkBook = id, Value = rate };
                _appDbContext.BookRates.Add(newRate);
                _appDbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private GetBookDTO ExtractBookDTO(Book book)
        {
            var authorsDtos = new List<AuthorInGetBookDTO>();
            var rateCount = book.Rates.Count();
            var averageRate = book.Rates.Count > 0 ? Math.Round(book.Rates.Average(b => b.Value), 1) : 0;

            book.Authors.ForEach(a => authorsDtos.Add(new AuthorInGetBookDTO(a.Id, a.FirstName, a.SecondName)));
            return new GetBookDTO(book.Id, book.Title, book.ReleaseDate, book.Description, averageRate, rateCount, authorsDtos);
        }

        private GetAuthorDTO ExtractAuthorDTO(Author author)
        {
            var authorsDtos = new List<BookInGetAuthorDTO>();
            var rateCount = author.Rates.Count();
            var averageRate = author.Rates.Count > 0 ? Math.Round(author.Rates.Average(b => b.Value), 1) : 0;

            author.Books.ForEach(a => authorsDtos.Add(new BookInGetAuthorDTO(a.Id, a.Title)));
            return new GetAuthorDTO(author.Id, author.FirstName, author.SecondName, author.Cv, averageRate, rateCount, authorsDtos);
        }
        public void StartupCreateIndex()
        {
            _elasticHelper.CreateIndex();
            CleanUpDatabase();
            GenerateAuthors();
            AddAuthorsToElastic();
            GenerateBooks();
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
                authors.Add(new Author(GenerateAuthorName(), GenerateAuthorSurname(), RandomString(1000)));
            }

            _appDbContext.Authors.AddRange(authors);
            _appDbContext.SaveChanges();

            var authorsWithIds = _appDbContext.Authors.ToList();

            foreach (var author in authorsWithIds)
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
                _elasticHelper.AddAuthorToElastic(ExtractAuthorDTO(author));
            }
        }

        private void AddBooksToElastic()
        {
            var books = _appDbContext.Books.Include("Authors")
                                            .Include("Rates")
                                            .ToList();

            foreach (var book in books)
            {
                _elasticHelper.AddBookToElastic(ExtractBookDTO(book));
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
                var book = new Book(title, DateTime.Now, RandomString(1000));
                for (int i = 0; i < random.Next(1, 5); i++)
                {
                    book.Authors.Add(authors[random.Next(0, authors.Count() - 1)]);
                }
            }

            _appDbContext.Books.AddRange(books);
            _appDbContext.SaveChanges();
            var booksWithIds = _appDbContext.Books.ToList();

            foreach (var book in booksWithIds)
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

        public string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}

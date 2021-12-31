using Model.DTOs;
using Nest;
using System.Collections.Generic;
using System.Linq;

namespace RepositoryPattern.Helpers
{
    public class ElasticHelper : IElasticHelper
    {
        private readonly IElasticClient _elasticClient;

        public ElasticHelper(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public GetAuthorDTO AddAuthorToElastic(GetAuthorDTO author)
        {
            IndexResponse response = _elasticClient.IndexDocument(author);
            return _elasticClient.Get<GetAuthorDTO>(response.Id).Source;
        }

        public GetBookDTO AddBookToElastic(GetBookDTO book)
        {
            IndexResponse response = _elasticClient.IndexDocument(book);
            return _elasticClient.Get<GetBookDTO>(response.Id).Source;
        }

        public void CreateIndex()
        {
            if (_elasticClient.Indices.Exists("get_book").Exists)
            {
                _elasticClient.Indices.Delete("get_book");
            }

            if (_elasticClient.Indices.Exists("get_author").Exists)
            {
                _elasticClient.Indices.Delete("get_author");
            }
        }

        public bool DeleteAuthorFromElastic(int id)
        {
            return _elasticClient.Delete<GetAuthorDTO>(id).Result == Result.Deleted;
        }

        public bool DeleteBookFromElastic(int id)
        {
            return _elasticClient.Delete<GetBookDTO>(id).Result == Result.Deleted;
        }

        public IEnumerable<GetAuthorDTO> GetAuthorFromElastic(int id = 0, SearchAuthorDTO searchAuthor = null, PaginationDTO pagination = null)
        {
            if (searchAuthor == null && id <= 0)
            {
                var searchRequest = new SearchRequest<GetAuthorDTO>
                {
                    From = pagination.Count * pagination.Page,
                    Size = pagination.Count,
                    Query = new MatchAllQuery()
                };
                return _elasticClient.Search<GetAuthorDTO>(searchRequest).Documents;
            }

            if (id > 0)
            {
                return new List<GetAuthorDTO>() { _elasticClient.Get<GetAuthorDTO>(id).Source };
            }

            var query = searchAuthor.MatchAll ? $"\"{searchAuthor.MasterQuery}\"" : $"*{searchAuthor.MasterQuery}*";

            var searchResult = _elasticClient.Search<GetAuthorDTO>(s => s.Query(
                                                                 q => q.MultiMatch(
                                                                 mm => mm.Fields(
                                                                 fs => fs.Field(f => f.FirstName, 10.0)
                                                                 .Field(f => f.SecondName, 7.0)
                                                                 .Field(f => f.CV, 5.0)
                                                                 .Field(f => f.Books.Select(a => a.Title), 3.0))
                                                                 .Fuzziness(Fuzziness.Auto)
                                                                 .Query(query))));
            return searchResult.Documents;

        }

        public IEnumerable<GetBookDTO> GetBookFromElastic(int id = 0, SearchBookDTO searchBook = null, PaginationDTO pagination = null)
        {

            if (searchBook == null && id <= 0)
            {
                var searchRequest = new SearchRequest<GetBookDTO>
                {
                    From = pagination.Count * pagination.Page,
                    Size = pagination.Count,
                    Query = new MatchAllQuery()
                };
                return _elasticClient.Search<GetBookDTO>(searchRequest).Documents;
            }

            if (id > 0)
            {
                return new List<GetBookDTO>() { _elasticClient.Get<GetBookDTO>(id).Source };
            }

            var query = searchBook.MatchAll ? $"\"{searchBook.MasterQuery}\"" : $"*{searchBook.MasterQuery}*";

            var searchResult = _elasticClient.Search<GetBookDTO>(s => s.Query(
                                                                 q => q.MultiMatch(
                                                                 mm => mm.Fields(
                                                                 fs => fs.Field(f => f.Title, 10.0)
                                                                 .Field(f => f.Description, 7.0)
                                                                 .Field(f => f.Authors.Select(a => a.FirstName), 5.0)
                                                                 .Field(f => f.Authors.Select(a => a.SecondName), 3.0))
                                                                 .Fuzziness(Fuzziness.Auto)
                                                                 .Query(query))));
            return searchResult.Documents;
        }

        public bool UpdateAuthorRateInElastic(int id, double averageRate, int count)
        {

            return _elasticClient.Update<GetAuthorDTO>(id, u => u.Index("get_author").Doc(new GetAuthorDTO(id, averageRate, count))).Result == Result.Updated;
        }

        public bool UpdateBookRateInElastic(int id, double averageRate, int count)
        {
            return _elasticClient.Update<GetBookDTO>(id, u => u.Index("get_book").Doc(new GetBookDTO(id, averageRate, count))).Result == Result.Updated;
        }

    }
}

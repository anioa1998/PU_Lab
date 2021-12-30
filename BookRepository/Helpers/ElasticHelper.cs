using Model.DTOs;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            if(_elasticClient.Indices.Exists("get_book").Exists)
            {
               _elasticClient.Indices.Delete("get_book");
            }

            if (_elasticClient.Indices.Exists("get_author").Exists)
            {
                _elasticClient.Indices.Delete("get_author");
            }
        }

        public IEnumerable<GetAuthorDTO> GetAuthor(int id = 0, PaginationDTO pagination = null)
        {
            if (id > 0)
            {
                return new List<GetAuthorDTO>() { _elasticClient.Get<GetAuthorDTO>(id).Source };
            }
            else
            {
                var searchRequest = new SearchRequest<GetAuthorDTO>
                {
                    From = pagination.Count * pagination.Page,
                    Size = pagination.Count,
                    Query = new MatchAllQuery()
                };
                return _elasticClient.Search<GetAuthorDTO>(searchRequest).Documents;
               
            }
        }

        public IEnumerable<GetBookDTO> GetBook(int id = 0, PaginationDTO pagination = null)
        {

            if (id > 0)
            {
                return new List<GetBookDTO>() { _elasticClient.Get<GetBookDTO>(id).Source };
            }
            else
            {
                var searchRequest = new SearchRequest<GetBookDTO>
                {
                    From = pagination.Count * pagination.Page,
                    Size = pagination.Count,
                    Query = new MatchAllQuery()
                };
                return _elasticClient.Search<GetBookDTO>(searchRequest).Documents;

            }
        }
    }
}

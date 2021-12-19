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
            if(_elasticClient.Indices.Exists("index_books").Exists)
            {
               _elasticClient.Indices.Delete("index_books");
            }

            if(_elasticClient.Indices.Exists("index_authors").Exists)
            {
                _elasticClient.Indices.Delete("index_authors");
            }

            _elasticClient.Indices.Create("index_books", index => index.Map<GetBookDTO>(x => x.AutoMap()));
            _elasticClient.Indices.Create("index_authors", index => index.Map<GetAuthorDTO>(x => x.AutoMap()));

        }
    }
}

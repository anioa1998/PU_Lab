using Model.DTOs;
using Nest;
using System;

namespace ProgramowanieUzytkoweIP12.Configuration
{
    public class ElasticConnection : ConnectionSettings
    {
        public ElasticConnection(Uri uri = null) : base(uri)
        {
            this.DefaultMappingFor<GetAuthorDTO>(x => x.IndexName("get_author"));
            this.DefaultMappingFor<GetBookDTO>(x => x.IndexName("get_book"));
        }

    }
}

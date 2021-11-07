using CQRS;
using CQRS.AuthorFiles.Commands;
using CQRS.AuthorFiles.Queries;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramowanieUzytkoweIP12.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CQRSAuthorController : Controller
    {
        private readonly CommandBus _commandBus;
        private readonly QueryBus _queryBus;

        public CQRSAuthorController(CommandBus commandBus, QueryBus queryBus)
        {
            _commandBus = commandBus;
            _queryBus = queryBus;
        }

        [HttpGet("GetAuthors")]
        public IEnumerable<GetAuthorDTO> GetAuthors([FromQuery] GetAuthorsQuery query)
        {
            return _queryBus.Handle<GetAuthorsQuery, List<GetAuthorDTO>>(query);
        }

        [HttpPost("AddAuthor")]
        public void AddAuthor([FromBody] AddAuthorCommand command)
        {
            _commandBus.Handle(command);
        }

        [HttpDelete("DeleteAuthor")]
        public void DeleteAuthor([FromQuery] int id)
        {
            _commandBus.Handle(new DeleteAuthorCommand(id));
        }

        [HttpPost("AddRate")]
        public void AddRate([FromBody] AddAuthorRateCommand command)
        {
            _commandBus.Handle(command);
        }
    }
}

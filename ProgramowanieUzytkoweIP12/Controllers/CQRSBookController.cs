using CQRS;
using CQRS.BookFiles.Commands;
using CQRS.BookFiles.Queries;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs;
using System.Collections.Generic;

namespace ProgramowanieUzytkoweIP12.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CQRSBookController : Controller
    {
        private readonly CommandBus _commandBus;
        private readonly QueryBus _queryBus;

        public CQRSBookController(CommandBus commandBus, QueryBus queryBus)
        {
            _commandBus = commandBus;
            _queryBus = queryBus;
        }

        [HttpGet("GetBooks")]
        public IEnumerable<GetBookDTO> GetBooks([FromQuery] GetBooksQuery query)
        {
            return _queryBus.Handle<GetBooksQuery, List<GetBookDTO>>(query);
        }

        [HttpGet("GetBook")]
        public GetBookDTO GetBook([FromQuery] GetBookQuery query)
        {
            return _queryBus.Handle<GetBookQuery, GetBookDTO>(query);
        }
        [HttpPost("AddBook")]
        public void AddBook([FromBody] AddBookCommand command)
        {
            _commandBus.Handle(command);
        }

        [HttpDelete("DeleteBook")]
        public void DeleteBook([FromQuery] int id)
        {
            _commandBus.Handle(new DeleteBookCommand(id));
        }

        [HttpPost("AddRate")]
        public void AddRate([FromBody] AddBookRateCommand command)
        {
            _commandBus.Handle(command);
        }
    }
}

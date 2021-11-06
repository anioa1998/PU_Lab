using CQRS;
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
    public class CQRSController : Controller
    {
        private readonly CommandBus _commandBus;
        private readonly QueryBus _queryBus;

        public CQRSController(CommandBus commandBus, QueryBus queryBus)
        {
            _commandBus = commandBus;
            _queryBus = queryBus;
        }

        [HttpGet]
        public IEnumerable<BookDTO> GetBooks([FromQuery] GetBooksQuery query)
        {
            return _queryBus.Handle<GetBooksQuery, List<BookDTO>>(query);
        }

        [HttpPost]
        public void Post([FromBody] AddBookCommand command)
        {
            _commandBus.Handle(command);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _commandBus.Handle(new DeleteBookCommand(id));
        }

    }
}

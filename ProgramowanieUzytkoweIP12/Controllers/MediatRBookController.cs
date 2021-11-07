using MediatR;
using MediatRProject.BookFiles.Commands;
using MediatRProject.BookFiles.Queries;
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
    public class MediatRBookController : Controller
    {
        private readonly IMediator _mediator;

        public MediatRBookController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetBooks")]
        public Task<List<GetBookDTO>> GetBooks(PaginationDTO paginationDTO)
        {
            return _mediator.Send(new GetMediatRBooksQuery(paginationDTO));
        }

        [HttpGet("GetBook")]
        public Task<GetBookDTO> GetBook([FromQuery] GetMediatRBookQuery query)
        {
            return _mediator.Send(query);
        }
        [HttpPost("AddBook")]
        public Task<bool> AddBook([FromBody] DeleteMediatRBookCommand command)
        {
            return _mediator.Send(command);
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

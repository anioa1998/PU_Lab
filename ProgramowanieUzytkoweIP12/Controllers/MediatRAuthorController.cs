using MediatR;
using MediatRProject.AuthorFiles.Commands;
using MediatRProject.AuthorFiles.Queries;
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
    public class MediatRAuthorController : Controller
    {
        private readonly IMediator _mediator;

        public MediatRAuthorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAuthors")]
        public Task<List<GetAuthorDTO>> GetAuthors([FromQuery] GetMediatRAuthorsQuery query)
        {
            return _mediator.Send(query);
        }

        [HttpPost("AddAuthor")]
        public Task<bool> AddAuthor([FromBody] AddMediatRAuthorCommand command)
        {
            return _mediator.Send(command);
        }

        [HttpDelete("DeleteAuthor")]
        public Task<bool> DeleteAuthor([FromQuery] DeleteMediatRAuthorCommand command)
        {
            return _mediator.Send(command);
        }

        [HttpPost("AddRate")]
        public Task<bool> AddRate([FromBody] AddMediatRAuthorRateCommand command)
        {
            return _mediator.Send(command);
        }
    }
}

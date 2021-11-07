using MediatR;
using Microsoft.AspNetCore.Mvc;
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
    }
}

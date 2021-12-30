using Microsoft.AspNetCore.Mvc;
using RepositoryPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramowanieUzytkoweIP12.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StartController : Controller
    {
        private readonly IStartRepository _startRepository;

        public StartController(IStartRepository startRepository)
        {
            _startRepository = startRepository;
        }

        [HttpGet("CreateIndex")]
        public ActionResult CreateIndex()
        {
            try
            {
                _startRepository.StartupCreateIndex();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

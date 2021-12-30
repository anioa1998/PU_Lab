using Microsoft.AspNetCore.Mvc;
using Model.DTOs;
using RepositoryPattern;
using System.Collections.Generic;


namespace ProgramowanieUzytkoweIP12.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : Controller
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorController(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        [HttpGet("All")]
        public ActionResult<IEnumerable<GetAuthorDTO>> GetAuthors([FromQuery] PaginationDTO pagination)
        {
            return _authorRepository.GetAuthors(pagination);
        }
        [HttpGet]
        public ActionResult<GetAuthorDTO> GetAuthor([FromQuery] SearchAuthorDTO author)
        {
            return _authorRepository.GetAuthor(author.Id);
        }

        [HttpPost]
        public ActionResult AddAuthor(AddAuthorDTO author)
        {
            var result = _authorRepository.AddAuthor(author);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Unable to add new author. Try again.");
            }
        }
        [HttpDelete]
        public ActionResult DeleteAuthor([FromQuery]int id)
        {
            var result = _authorRepository.DeleteAuthor(id);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost("Rate")]
        public ActionResult AddRate([FromQuery] int id, [FromQuery] short rate)
        {
            var result = _authorRepository.AddRate(id, rate);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}

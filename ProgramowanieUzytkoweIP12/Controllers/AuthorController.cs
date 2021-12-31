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
        [HttpGet("{id}")]
        public ActionResult<GetAuthorDTO> GetAuthor([FromRoute]int id)
        {
            return _authorRepository.GetAuthor(id);
        }

        [HttpGet("Search")]
        public ActionResult<IEnumerable<GetAuthorDTO>> SearchAuthors([FromQuery] SearchAuthorDTO searchAuthor)
        {
            return _authorRepository.SearchAuthors(searchAuthor);
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
        [HttpDelete("{id}")]
        public ActionResult DeleteAuthor([FromRoute]int id)
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
        [HttpPost("{id}/Rate")]
        public ActionResult AddRate([FromRoute] int id, [FromQuery] short rate)
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

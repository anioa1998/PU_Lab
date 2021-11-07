using Microsoft.AspNetCore.Mvc;
using Model.DTOs;
using RepositoryPattern;
using System.Collections.Generic;


namespace ProgramowanieUzytkoweIP12.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorController(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        [HttpGet("GetAuthors")]
        public ActionResult<IEnumerable<GetAuthorDTO>> GetAuthors([FromQuery] PaginationDTO pagination)
        {
            return _authorRepository.GetAuthors(pagination);
        }

        [HttpPost("AddAuthor")]
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
        [HttpDelete("DeleteAuthor/{id}")]
        public ActionResult DeleteAuthor(int id)
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
        [HttpPost("AddRate")]
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

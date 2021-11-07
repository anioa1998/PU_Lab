using Microsoft.AspNetCore.Mvc;
using Model.DTOs;
using System.Collections.Generic;


namespace ProgramowanieUzytkoweIP12.Controllers
{
    public class AuthorController : Controller
    {
        //private readonly IAuthorRepository _authorRepository;

        //public AuthorController(IAuthorRepository authorRepository)
        //{
        //    _authorRepository = authorRepository;
        //}

        [HttpGet("GetAuthors")]
        public ActionResult<IEnumerable<GetAuthorDTO>> GetBooks([FromQuery] PaginationDTO pagination)
        {
            return Ok();
        }

        [HttpGet("GetAuthor/{id}")]
        public ActionResult<GetBookDTO> GetBook(int id)
        {
            return Ok();
        }
        [HttpPost("AddAuthor")]
        public ActionResult AddBook(GetBookDTO book)
        {
            return Ok();
        }
        [HttpDelete("DeleteAuthor/{id}")]
        public ActionResult DeleteBook(int id)
        {
            return Ok();
        }
        [HttpPost("AddRate")]
        public ActionResult AddRate([FromQuery] int id, [FromQuery] int rate)
        {
            return Ok();
        }
    }
}

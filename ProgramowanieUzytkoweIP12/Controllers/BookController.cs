using Microsoft.AspNetCore.Mvc;
using Model.DTOs;
using RepositoryPattern;
using System.Collections.Generic;

namespace ProgramowanieUzytkoweIP12.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet("GetBooks")]
        public ActionResult<IEnumerable<BookDTO>> GetBooks([FromQuery] PaginationDTO pagination)
        {
            return _bookRepository.GetBooks();
        }

        [HttpGet("GetBook/{id}")]
        public ActionResult<BookDTO> GetBook(int id)
        {
            return _bookRepository.GetBook(id);
        }
        [HttpPost("AddBook")]
        public ActionResult AddBook(BookDTO book)
        {
            return Ok();
        }
        [HttpDelete("DeleteBook/{id}")]
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

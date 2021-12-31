using Microsoft.AspNetCore.Mvc;
using Model.DTOs;
using Nest;
using RepositoryPattern;
using System;
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

        [HttpGet("All")]
        public ActionResult<IEnumerable<GetBookDTO>> GetBooks([FromQuery] PaginationDTO pagination)
        {
            return _bookRepository.GetBooks(pagination);
        }

        [HttpGet("{id}")]
        public ActionResult<GetBookDTO> GetBook([FromRoute] int id)
        {
            return _bookRepository.GetBook(id);
        }

        [HttpGet("Search")]
        public ActionResult<List<GetBookDTO>> SearchBooks([FromQuery] SearchBookDTO searchBook)
        {
            return _bookRepository.SearchBooks(searchBook);
        }
        [HttpPost]
        public ActionResult AddBook([FromBody] AddBookDTO book)
        {

            var result = _bookRepository.AddBook(book);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Unable to add new book. Try again.");
            }

        }
        [HttpDelete("{id}")]
        public ActionResult DeleteBook([FromRoute] int id)
        {
            var result = _bookRepository.DeleteBook(id);
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
            var result = _bookRepository.RateBook(id, rate);
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

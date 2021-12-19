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

        [HttpGet("GetBooks")]
        public ActionResult<IEnumerable<GetBookDTO>> GetBooks([FromQuery] PaginationDTO pagination)
        {
            return _bookRepository.GetBooks(pagination);
        }

        [HttpGet("GetBook")]
        public ActionResult<GetBookDTO> GetBook([FromQuery] int id)
        {
            return _bookRepository.GetBook(id);
        }
        [HttpPost("AddBook")]
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
        [HttpDelete("DeleteBook")]
        public ActionResult DeleteBook([FromQuery] int id)
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
        [HttpPost("AddRate")]
        public ActionResult AddRate([FromQuery] int id, [FromQuery] short rate)
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

        [HttpGet("CreateIndex")]
        public ActionResult CreateIndex()
        {
            try
            {
                _bookRepository.StartupCreateIndex();
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}

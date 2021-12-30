﻿using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public ActionResult<GetBookDTO> GetBook([FromQuery] SearchBookDTO book)
        {
            return _bookRepository.GetBook(book.Id);
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
        [HttpDelete]
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
        [HttpPost("Rate")]
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

       


    }
}

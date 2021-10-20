using AthensLibrary.Data.Interface;
using AthensLibrary.Model.DataTransferObjects;
using AthensLibrary.Model.DataTransferObjects.AuthorControllerDTO;
using AthensLibrary.Model.Entities;
using AthensLibrary.Service.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthensLibrary.Controllers
{
    [Route("api/author")]
    [ApiController]
    public class AuthorController : ControllerBase
    {       
        private readonly IAuthorService _authorService;
        private readonly IServiceFactory _serviceFactory;

        public AuthorController(IServiceFactory serviceFactory, IAuthorService authorService)
        {           
            _authorService = authorService;
            _serviceFactory = serviceFactory;           
        }

        [HttpGet("Authors")]
        public IActionResult GetAllAuthors()
        {
            var author = _authorService.GetAllAuthors();
            return Ok(author);
        }

        [HttpGet("AuthorbyId/{id}")]
        public IActionResult GetAuthorById(Guid Id)
        {
            /*var author = _authorService.GetById(Id);
            var authorDto = _mapper.Map<AuthorDTO>(author); //please let the service be able to do the mapping and return the dto here*/
            return Ok();

            //return Ok(_authorService.GetById(Id)) //this should be the only hting in this action  method
        }

        [HttpPost("Create")]
        public async  Task<IActionResult> CreateBook(BookCreationDTO model)
        {
            if (!ModelState.IsValid) return BadRequest("Object sent from client is null.");
            var bookService = _serviceFactory.GetServices<IBookService>();
            var (success, message) = await bookService.CreateBook(model);
            return success ? Ok(message) : BadRequest(message);
        }
    }
}

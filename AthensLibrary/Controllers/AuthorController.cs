using AthensLibrary.Data.Interface;
using AthensLibrary.Model.DataTransferObjects;
using AthensLibrary.Model.Entities;
using AthensLibrary.Service.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AthensLibrary.Controllers
{
    [Route("api/author")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IUnitOfWork _unitofWork;
        private readonly IMapper _mapper;
        private readonly IAuthorService _authorService;
        private readonly IBookService _bookService;
        private readonly IServiceFactory _serviceFactory;

        public AuthorController(IUnitOfWork unitofWork, IServiceFactory serviceFactory, IMapper mapper, IAuthorService authorService, IBookService bookService)
        {
            _unitofWork = unitofWork;
            _authorService = authorService;
            _serviceFactory = serviceFactory;
            _mapper = mapper;
            _bookService = bookService;
        }

        [HttpGet, Authorize(Policy = "AdminRolePolicy")]
        public IActionResult GetAllAuthors()
        {
            var author = _authorService.GetAllAuthors();
            return Ok(author);
        }

        [HttpGet("Id/{id}"), Authorize(Policy ="AdminRolePolicy")]
        public IActionResult GetAuthorById(Guid Id)
        {
            var author = _authorService.GetById(Id);
            var authorDto = _mapper.Map<AuthorDto>(author);
            return Ok(authorDto);
        }

        [HttpPost("createbook"), Authorize(Policy = "AdminRolePolicy", Roles = "Author")]
        public IActionResult CreateBook(Book book)
        {
            var bookEntity = _mapper.Map<Book>(book);
            _bookService.CreateBook(bookEntity);
            return Ok("Book created Successfully");
        }

        [HttpGet("email/{email}"), Authorize(Policy = "AdminRolePolicy")]
        public IActionResult GetAuthorByEmail(string email)
        {
            var author = _authorService.GetAuthorsByEmail(email);
            var authorDto = _mapper.Map<AuthorDto>(author);
            return Ok(authorDto);
        }

    }
}

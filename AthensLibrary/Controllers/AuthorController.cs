using AthensLibrary.Data.Interface;
using AthensLibrary.Model.DataTransferObjects;
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
        private readonly IUnitofWork _unitofWork;
        private readonly IMapper _mapper;
        private readonly IAuthorService _authorService;
        private readonly IServiceFactory _serviceFactory;

        public AuthorController(IUnitofWork unitofWork, IServiceFactory serviceFactory, IMapper mapper, IAuthorService authorService)
        {
            _unitofWork = unitofWork;
            _authorService = authorService;
            _serviceFactory = serviceFactory;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllAuthors()
        {
            var author = _authorService.GetAllAuthors();
            return Ok(author);
        }

        [HttpGet("Id/{id}")]
        public IActionResult GetAuthorById(Guid Id)
        {
            var author = _authorService.GetById(Id);
            var authorDto = _mapper.Map<AuthorDto>(author);
            return Ok(authorDto);
        }

        [HttpGet("email/{email}")]
        public IActionResult GetAuthorByEmail(string email)
        {
            var author = _authorService.GetAuthorsByEmail(email);
            var authorDto = _mapper.Map<AuthorDto>(author);
            return Ok(authorDto);
        }

    }
}

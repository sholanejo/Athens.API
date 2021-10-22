using AthensLibrary.Data.Interface;
using AthensLibrary.Filters.AuthorizationFilters;
using AthensLibrary.Model.DataTransferObjects;
using AthensLibrary.Model.DataTransferObjects.AuthorControllerDTO;
using AthensLibrary.Model.DataTransferObjects.LibraryUserControllerDTO;
using AthensLibrary.Model.Entities;
using AthensLibrary.Service.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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

        [HttpPost(Name ="AddAuthor"), Authorize(Policy = "AdminRolePolicy")]
        //How can this method be generic to both libraryusers and authors
        public async Task<IActionResult> AddAuthor(UserRegisterDTO model)
        {
            var _userService = _serviceFactory.GetServices<IUserService>();
            var (success, message) = await _userService.EnrollAuthor(model);
            return success ? Ok(message) : BadRequest(message);
        }
        [HttpGet(Name ="GetAuthors")]
        public IActionResult GetAllAuthors()
        {
            var author = _authorService.GetAllAuthors();
            return Ok(author);
        }

        [HttpGet("Id/{id}"), Authorize(Policy = "AdminRolePolicy")] 
        public IActionResult GetAuthorById(Guid Id)
        {
            /*var author = _authorService.GetById(Id);
            var authorDto = _mapper.Map<AuthorDTO>(author); //please let the service be able to do the mapping and return the dto here*/
            return Ok();

            //return Ok(_authorService.GetById(Id)) //this should be the only hting in this action  method
        }
        //Get author by name        
    }
}

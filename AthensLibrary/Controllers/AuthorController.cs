using System;
using System.Threading.Tasks;
using AthensLibrary.Model.DataTransferObjects.LibraryUserControllerDTO;
using AthensLibrary.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            return Ok(_authorService.GetAuthorById(Id));
        }


        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var (success, message)= await _authorService.Delete(id);
            return success ? Ok(message) : BadRequest(message);

        }

        [HttpGet("name/{name}")]
        public IActionResult GetAuthorByName(string name)
        {           
            return Ok( _authorService.GetAuthorByName(name));
        }
    }
}

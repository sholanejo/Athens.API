using System;
using System.Threading.Tasks;
using AthensLibrary.Filters.ActionFilters;
using AthensLibrary.Model.DataTransferObjects.LibraryUserControllerDTO;
using AthensLibrary.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AthensLibrary.Controllers
{
    [Authorize(Policy = "AdminRolePolicy")]
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

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost(Name = "AddAuthor")]
        //How can this method be generic to both libraryusers and authors
        public async Task<IActionResult> AddAuthor(UserRegisterDTO model)
        {
            var _userService = _serviceFactory.GetServices<IUserService>();

            var result = await _userService.EnrollAuthor(model);

            if (result.Success)
                return Ok(result.Message);

            return BadRequest(result.Message);
        }


        [HttpGet("id/{id}")]
        public IActionResult GetAuthorById(Guid Id)
        {
            return Ok(_authorService.GetAuthorById(Id));
        }


        [HttpDelete("id/{id}")]
        public IActionResult Delete(Guid id)
        {
            var result = _authorService.Delete(id);

            return (result.Success) ? Ok(result.Message) : BadRequest(result.Message);
        }

        [AllowAnonymous]
        [HttpGet("name/{name}")]
        public IActionResult GetAuthorByName(string name)
        {
            return Ok(_authorService.GetAuthorByName(name));
        }

        [AllowAnonymous]
        [HttpGet(Name = "GetAuthors")]
        public IActionResult GetAllAuthors()
        {
            var author = _authorService.GetAllAuthors();
            return Ok(author);
        }
    }
}

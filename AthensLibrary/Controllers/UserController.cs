using System;
using System.Text;
using System.Threading.Tasks;
using AthensLibrary.Data.Interface;
using AthensLibrary.Filters.ActionFilters;
using AthensLibrary.Model.DataTransferObjects.LibraryUserControllerDTO;
using AthensLibrary.Model.Entities;
using AthensLibrary.Model.RequestFeatures;
using AthensLibrary.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AthensLibrary.Controllers
{
   // [ServiceFilter(typeof(ValidationFilterAttribute))]
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IServiceFactory _serviceFactory;
        private readonly IUserService _userService;

        public UserController(IServiceFactory serviceFactory, IUserService userService)
        {
            _serviceFactory = serviceFactory;
            _userService = userService;
        }

        //POST
        [HttpPost(Name ="Login")]
        public async Task<IActionResult> Authenticate([FromBody] UserLoginDTO user)
        {   
            var _authManager = _serviceFactory.GetServices<IAuthentication>();
            var _userManager = _serviceFactory.GetServices<UserManager<User>>();
            if (!await _authManager.ValidateUser(user)) { return Unauthorized($"{nameof(Authenticate)}: Authentication failed. Wrong user name or password."); }
            var loggedInUser = await _userManager.FindByEmailAsync(user.Email);
            HttpContext.Session.Set("Email", Encoding.ASCII.GetBytes(loggedInUser.Email));
            return Ok(new { Token = await _authManager.CreateToken() });
        }        

        //Patch
        [HttpPatch("Update"), Authorize]
        public async Task<IActionResult> UpdateUser([FromBody] JsonPatchDocument<UserUpdateDTO> model)
        {
            HttpContext.Session.TryGetValue("Email", out byte[] email);
            if (email.Length == 0) return NotFound("Email from last session not found");
            var (success, message) = await _userService.UpdateUser(Encoding.UTF8.GetString(email), model);
            return success ? Ok(message) : BadRequest(message);
        }

        [HttpPatch("Update/{Id}"), Authorize(Policy = "AdminRolePolicy")]
        public async Task<IActionResult> UpdateUser(string Id, [FromBody] JsonPatchDocument<UserUpdateDTO> model)
        {
            var (success, message) = await _userService.UpdateUser(Id, model);
            return success ? Ok(message) : BadRequest(message);
        }

        //GET
        [HttpGet("Books")]
        public IActionResult GetAllBooks() => Ok(_userService.GetAllBooks());
        [HttpGet("BooksByAuthor/{authorId}")]
        public IActionResult GetAllBooksByAnAuthor(string authorId) => Ok(_userService.GetAllBooksByAnAuthor(authorId));
        public IActionResult GetAllBooksByLoggedInAuthor()
        {
            HttpContext.Session.TryGetValue("Email", out byte[] email);
            return Ok(_userService.GetAllBooksByAnAuthor(Encoding.ASCII.GetString(email)));
        }
        [HttpGet("BooksByCategory/{categoryName}")]
        public IActionResult GetAllBooksByACategory(string categoryName) => Ok(_userService.GetAllBooksInACategory(categoryName));
        [HttpGet("BooksByYear/{year}")]
        public IActionResult GetAllBooksByYearPublished(int year) => Ok(_userService.GetAllBooksPublishedInAYear(year));
        [HttpGet("BooksByTitle/{bookTitle}")]
        public IActionResult GetAllBooksByTitle(string bookTitle) => Ok(_userService.GetBooksByTitle(bookTitle));
        [HttpGet("Book/{Id}")]
        public IActionResult GetBookByIsbn(Guid Id) => Ok(_userService.GetABookByIsbn(Id));

        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(Guid id)
        {
            _userService.Delete(id);
            return Ok();
        }

    }
}

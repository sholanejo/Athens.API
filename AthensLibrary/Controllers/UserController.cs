using System;
using System.Text;
using System.Threading.Tasks;
using AthensLibrary.Data.Interface;
using AthensLibrary.Model.DataTransferObjects.LibraryUserControllerDTO;
using AthensLibrary.Model.Entities;
using AthensLibrary.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AthensLibrary.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IServiceFactory _serviceFactory;
        private readonly IUserService _userService;
        private readonly UserManager<User> userManager;

        public UserController(IServiceFactory serviceFactory, IUserService userService)
        {
            _serviceFactory = serviceFactory;
            _userService = userService;
          }

        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] UserLoginDTO user)
        {
            var _authManager = _serviceFactory.GetServices<IAuthentication>();
            var _userManager = _serviceFactory.GetServices<UserManager<User>>();
            if (!ModelState.IsValid) return BadRequest("Object sent from client is null.");
            if (!await _authManager.ValidateUser(user)) { return Unauthorized($"{nameof(Authenticate)}: Authentication failed. Wrong user name or password."); }
            var loggedInUser = await _userManager.FindByEmailAsync(user.Email);
            HttpContext.Session.Set("Email", Encoding.ASCII.GetBytes(loggedInUser.Email));
            return Ok(new { Token = await _authManager.CreateToken() });
        }

        [HttpPut("Update"), Authorize]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdateDTO user)
        {
            if (!ModelState.IsValid) return BadRequest("Object sent from client is null.");
            HttpContext.Session.TryGetValue("Email", out byte[] email);
            if (email.Length == 0) return NotFound("Email from last session not found");
            var (success, message) = await _userService.UpdateUser(Encoding.UTF8.GetString(email), user);
            return success ? Ok(message) : BadRequest(message);
        }
        [HttpPut("UpdateUser"), Authorize(Policy = "AdminRolePolicy")]
        public async Task<IActionResult> UpdateUser(string userId, [FromBody] UserUpdateDTO user)
        {
            if (!ModelState.IsValid) return BadRequest("Object sent from client is null.");
            var (success, message) = await _userService.UpdateUser(userId, user);
            return success ? Ok(message) : BadRequest(message);
        }
        [HttpGet("Books")]
        public IActionResult GetAllBooks() =>  Ok(_userService.GetAllBooks());
        [HttpGet("BooksByAuthor/{authorId}")]
        public IActionResult GetAllBooksByAnAuthor(string authorId) =>  Ok(_userService.GetAllBooksByAnAuthor(authorId));
        [HttpGet("BooksByCategory/{categoryName}")]
        public IActionResult GetAllBooksByACategory(string categoryName ) =>  Ok(_userService.GetAllBooksByAnAuthor(categoryName));
        [HttpGet("BooksByYear/{year}")]
        public IActionResult GetAllBooksByYearPublished(int year) =>  Ok(_userService.GetAllBooksPublishedInAYear(year));
        [HttpGet("BooksByTitle/{bookTitle}")]
        public IActionResult GetAllBooksByTitle(string bookTitle) =>  Ok(_userService.GetBooksByTitle(bookTitle));
        [HttpGet("Book/{Id}")]
        public IActionResult GetBooksByIsbn(Guid Id) =>  Ok(_userService.GetABookByIsbn(Id));
               
    }
}

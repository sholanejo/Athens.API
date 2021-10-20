using System;
using System.Text;
using System.Threading.Tasks;
using AthensLibrary.Data.Interface;
using AthensLibrary.Model.DataTransferObjects.LibraryUserControllerDTO;
using AthensLibrary.Model.Entities;
using AthensLibrary.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace AthensLibrary.Controllers
{
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

        [HttpPost("Enroll")]
        //How can this method be generic to both libraryusers and authors
        public async Task<IActionResult> EnrollAuthor(UserRegisterDTO model, string role)
        {
            if (!ModelState.IsValid) return BadRequest("Object sent from client is null.");           
            var (success, message) = await _userService.EnrollAuthor(model);
            return success ? Ok(message) : BadRequest(message);
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

        [HttpPost("{BorrowerId}/CheckOutABook"), Authorize] //How can we use multiple policies without need to chain in startup
        public async Task<IActionResult> CheckOutABook(string BorrowerId, [FromBody] CheckOutABookDTO model)
        {
            if (!ModelState.IsValid) return BadRequest("Object sent from client is null.");           
            var (success, message) = await _userService.CheckOutABook(BorrowerId, model);
            return success ? Ok(message) : BadRequest(message);
        }

        //PUT
        [HttpPut("{BorrowDetailId}/ReturnABook"), Authorize]
        public async Task<IActionResult> ReturnABook(Guid borrowDetailId)
        //what if i want to return a list of books
        {
            if (!ModelState.IsValid) return BadRequest("Object sent from client is null.");          
            var (success, message) = await _userService.ReturnABook(borrowDetailId);
            return success ? Ok(message) : BadRequest(message);
        }

        //Patch
        [HttpPatch("Update"), Authorize]
        public async Task<IActionResult> UpdateUser([FromBody] JsonPatchDocument<UserUpdateDTO> model)
        {
            if (!ModelState.IsValid) return BadRequest("Object sent from client is null.");
            HttpContext.Session.TryGetValue("Email", out byte[] email);
            if (email.Length == 0) return NotFound("Email from last session not found");
            var (success, message) = await _userService.UpdateUser(Encoding.UTF8.GetString(email),model);
            return success ? Ok(message) : BadRequest(message);
        }

        [HttpPatch("Update/{Id}"), Authorize(Policy ="AdminRolePolicy")]
        public async Task<IActionResult> UpdateUser(string Id, [FromBody] JsonPatchDocument<UserUpdateDTO> model)
        {
            if (!ModelState.IsValid) return BadRequest("Object sent from client is null.");           
            var (success, message) = await _userService.UpdateUser(Id,model);
            return success ? Ok(message) : BadRequest(message);
        }
        
        //GET
        [HttpGet("Books")]
        public IActionResult GetAllBooks() =>  Ok(_userService.GetAllBooks());
        [HttpGet("BooksByAuthor/{authorId}")]
        public IActionResult GetAllBooksByAnAuthor(string authorId) =>  Ok(_userService.GetAllBooksByAnAuthor(authorId));
        [HttpGet("BooksByCategory/{categoryName}")]
        public IActionResult GetAllBooksByACategory(string categoryName ) =>  Ok(_userService.GetAllBooksInACategory(categoryName));
        [HttpGet("BooksByYear/{year}")]
        public IActionResult GetAllBooksByYearPublished(int year) =>  Ok(_userService.GetAllBooksPublishedInAYear(year));
        [HttpGet("BooksByTitle/{bookTitle}")]
        public IActionResult GetAllBooksByTitle(string bookTitle) =>  Ok(_userService.GetBooksByTitle(bookTitle));
        [HttpGet("Book/{Id}")]
        public IActionResult GetBookByIsbn(Guid Id) =>  Ok(_userService.GetABookByIsbn(Id));
               
    }
}

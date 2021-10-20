using System;
using System.Text;
using System.Threading.Tasks;
using AthensLibrary.Data.Interface;
using AthensLibrary.Model.DataTransferObjects.LibraryUserControllerDTO;
using AthensLibrary.Model.Entities;
using AthensLibrary.Model.Enumerators;
using AthensLibrary.Model.Helpers.HelperClasses;
using AthensLibrary.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AthensLibrary.Controllers
{
    [Route("api/LibraryUser")]
    [ApiController]
    public class LibraryUserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthentication _authManager;
        private readonly IServiceFactory serviceFactory;

        public LibraryUserController(UserManager<User> userManager,
            SignInManager<User> signInManager,
            IUnitOfWork unitOfWork,
            IAuthentication authManager,
            IServiceFactory serviceFactory)
        {
            _userManager = userManager;           
            _unitOfWork = unitOfWork;
            _authManager = authManager;
            this.serviceFactory = serviceFactory;
        }

        [HttpPost("Register"), Authorize(Policy = "AdminRolePolicy")]
        public async Task<IActionResult> Register(UserRegisterDTO model)
        {
            //this method shoould use createAtRoute to return 201 and not 200, following rest pprinciples
            if (!ModelState.IsValid) return BadRequest("Object sent from client is null.");

            var user = new User
            {
                FullName = model.FullName,
                Email = model.Email,
                UserName = model.Email,
                CreatedBy = "Shola nejo",
                PhoneNumber = model.PhoneNumber,
                CreatedAt = DateTime.Now,
                DateOfBirth = model.DateOfBirth,
                Gender = model.Gender
            };
            var createUserResult = await _userManager.CreateAsync(user, model.Password);

            if (!createUserResult.Succeeded)
            {
                foreach (var error in createUserResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }            
            var addRoleResult = await _userManager.AddToRoleAsync(user, Roles.LibraryUser.ToString());
            if (!addRoleResult.Succeeded) return BadRequest("Role Add failed"); //delete created user!
            var libraryUser = new LibraryUser
            {
                BorrowerId = RandomItemGenerators.GenerateBorrowerId(),
                UserId = user.Id
            };
            var repo = _unitOfWork.GetRepository<LibraryUser>();
            repo.Add(libraryUser);
            _unitOfWork.SaveChanges();
            return Ok();
        }

        [HttpPost("{BorrowerId}/CheckOutABook"), Authorize] //How can we use multiple policies without need to chain in startup
        public async Task<IActionResult> CheckOutABook(string BorrowerId, [FromBody] CheckOutABookDTO model)
        {
            if (!ModelState.IsValid) return BadRequest("Object sent from client is null.");
            var _UserService = serviceFactory.GetServices<IUserService>();
            var (success, message) = await _UserService.CheckOutABook(BorrowerId, model);
            return success ? Ok(message) : BadRequest(message);
        }

        [HttpPut("{BorrowDetailId}/ReturnABook"), Authorize]
        public async Task<IActionResult> ReturnABook(Guid borrowDetailId)
        //what if i want to return a list of books
        {
            if (!ModelState.IsValid) return BadRequest("Object sent from client is null.");
            var _libraryUserService = serviceFactory.GetServices<IUserService>();
            var (success, message) = await _libraryUserService.ReturnABook(borrowDetailId);
            return success ? Ok(message) : BadRequest(message);
        }

        [HttpPut("{userId}/UpdateEmail")]
        public async Task<IActionResult> UpdateEmail(UserUpdateEmailDTO model)
        {
            if (!ModelState.IsValid) return BadRequest("Object sent from client is null.");
            HttpContext.Session.TryGetValue("Email", out byte[] emailByte);
            var user = await _userManager.FindByEmailAsync(Encoding.ASCII.GetString(emailByte));
            if (user is null) return BadRequest("Invalid User");
            user.Email = model.NewEmail;
            user.UserName = model.NewEmail;
            user.UpdatedAt = DateTime.Now;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded) return BadRequest("Email Change Failed");
            return Ok();
        }

        [HttpPut("UpdatePhoneNumber"), Authorize(Policy = "LibraryUserRolePolicy")]
        public async Task<IActionResult> UpdatePhoneNumber(UserUpdatePhoneNumberDTO model)
        //what if i want to return a list of books
        {
            if (!ModelState.IsValid) return BadRequest("Object sent from client is null");
            HttpContext.Session.TryGetValue("Email", out byte[] emailByte);
            var user = await _userManager.FindByEmailAsync(Encoding.ASCII.GetString(emailByte));
            if (user is null) return BadRequest("Invalid User");
            user.PhoneNumber = model.NewPhoneNumber;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded) return BadRequest("Email Change Failed");
            return Ok();
        }
        public async Task<IActionResult> UpdatePassword(UserUpdatePasswordDTO model)
        {
            if (!ModelState.IsValid) return BadRequest("Object sent from client is null.");
            HttpContext.Session.TryGetValue("Email", out byte[] emailByte);
            var user = await _userManager.FindByEmailAsync(Encoding.ASCII.GetString(emailByte));
            if (user is null) return BadRequest("Invalid User");
            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (!result.Succeeded) return BadRequest("PasswordChange Failed");
            return Ok();
        }

        [HttpGet("Books")]
        public IActionResult GetAllBooksInLibrary() => Ok(_unitOfWork.GetRepository<Book>().GetAll());

        [HttpGet("BooksByAnAuthor/{authorId}")]
        public IActionResult GetAllBooksByAnAuthor(Guid authorId) => Ok(_unitOfWork.GetRepository<Book>().GetByCondition(a => a.AuthorId == authorId));

        [HttpGet("BooksByCategory/{categoryName}")]
        public IActionResult GetAllBooksByCategory(string categoryName) => Ok(_unitOfWork.GetRepository<Book>().GetByCondition(a => a.CategoryName == categoryName));

        [HttpGet("BooksByPublishYear/{publishYear}")]
        public IActionResult GetAllBooksByPublishYear(int publishYear) => Ok(_unitOfWork.GetRepository<Book>().GetByCondition(a => a.PublicationYear.Year == publishYear));

        [HttpGet("BookByISBN/{Id}")]
        public IActionResult GetBookById(Guid Id) => Ok(_unitOfWork.GetRepository<Book>().GetByCondition(a => a.ID == Id));

        [HttpGet("BookByTitle/{bookTitle}")]
        public IActionResult GetBookByTitle(string bookTitle) => Ok(_unitOfWork.GetRepository<Book>().GetByCondition(a => a.Title == bookTitle));

    }
}

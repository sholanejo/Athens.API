using System.Text;
using System.Threading.Tasks;
using AthensLibrary.Data.Interface;
using AthensLibrary.Model.DataTransferObjects.LibraryUserControllerDTO;
using AthensLibrary.Model.Entities;
using AthensLibrary.Service.Interface;
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



    }
}

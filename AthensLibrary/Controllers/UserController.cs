using System.Text;
using System.Threading.Tasks;
using AthensLibrary.Data.Interface;
using AthensLibrary.Filters.ActionFilters;
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
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost(Name = "Login")]
        public async Task<IActionResult> Authenticate([FromBody] UserLoginDTO user)
        {
            var _authManager = _serviceFactory.GetServices<IAuthentication>();
            var _userManager = _serviceFactory.GetServices<UserManager<User>>();

            var loggedInUser = await _userManager.FindByEmailAsync(user.Email);
            if (loggedInUser is null)
                return NotFound("User Not found, please Register to continue");
            
            if (!await _authManager.ValidateUser(user))
                return Unauthorized($"{nameof(Authenticate)}: Authentication failed. Wrong user name or password.");
           
            HttpContext.Session.Set("Email", Encoding.ASCII.GetBytes(loggedInUser?.Email));
            return Ok(new { Token = await _authManager.CreateToken() });
        }

        //Patch
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPatch(Name = "Update"), Authorize]
        public async Task<IActionResult> UpdateUser([FromBody] JsonPatchDocument<UserUpdateDTO> model)
        {
           /* if (model is null) return BadRequest("model for update is null");
            if (!ModelState.IsValid) return UnprocessableEntity(ModelState);
*/
            HttpContext.Session.TryGetValue("Email", out byte[] email);

            if (email.Length == 0)
                return NotFound("Email from last session not found");

            var result = await _userService.UpdateUser(Encoding.UTF8.GetString(email), model);

            if (result.Success)
                return Ok(result.Message);

            return BadRequest(result.Message);
        }

        [HttpPatch("id/{Id}"), Authorize(Policy = "AdminRolePolicy")]
        public async Task<IActionResult> UpdateUser(string Id, [FromBody] JsonPatchDocument<UserUpdateDTO> model)
        {
            if (model is null) return BadRequest("model for update is null");
            if (!ModelState.IsValid) return UnprocessableEntity(ModelState);

            var result = await _userService.UpdateUser(Id, model);
            if (result.Success)
                return Ok(result.Message);

            return BadRequest(result.Message);
        }

    }
}

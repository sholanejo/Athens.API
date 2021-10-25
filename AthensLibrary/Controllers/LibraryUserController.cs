using System;
using System.Threading.Tasks;
using AthensLibrary.Filters.ActionFilters;
using AthensLibrary.Model.DataTransferObjects.LibraryUserControllerDTO;
using AthensLibrary.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AthensLibrary.Controllers
{

    [Route("api/LibraryUser")]
    [ApiController]
    public class LibraryUserController : ControllerBase
    {
        private readonly IServiceFactory _serviceFactory;
        private readonly ILibraryUserService _libraryUserService;

        public LibraryUserController(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
            _libraryUserService = _serviceFactory.GetServices<ILibraryUserService>();
        }

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost(Name = "Register")]
        public async Task<IActionResult> Register(UserRegisterDTO model)
        {
            //this method shoould use createAtRoute to return 201 and not 200, following rest pprinciples           
            var (success, message) = await _libraryUserService.Register(model);
            return success ? Ok(message) : BadRequest(message);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var (success, message) = await _libraryUserService.Delete(id);
            return success ? Ok(message) : BadRequest(message);
        }

        [HttpGet(Name = "GetLibraryUsers")]
        public IActionResult GetAllLibraryUsers()
        {
            var LibraryUser = _libraryUserService.GetAllLibraryUsers();
            return Ok(LibraryUser);
        }

        [HttpGet("Id/{id}"), Authorize(Policy = "AdminRolePolicy")]

        public IActionResult GetLibraryUserById(Guid Id)
        {
            return Ok(_libraryUserService.GetLibraryUserById(Id));
        }

        [HttpGet("Name/{name}"), Authorize(Policy = "AdminRolePolicy")]
        public IActionResult GetLibraryUserByName(string name)
        {
            var result = _libraryUserService.GetLibraryUserByName(name);
            if (result is null) return NotFound("Library User not found");
            return Ok(result);
        }
    }
}

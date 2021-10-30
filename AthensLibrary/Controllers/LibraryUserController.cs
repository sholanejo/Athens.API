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
       
        [HttpDelete("{id}")]
        public IActionResult DeleteLibraryUser(Guid id)
        {
            var result = _libraryUserService.Delete(id);

            if (result.Success)
                return Ok(result.Message);

            return BadRequest(result.Message);
        }

        [HttpGet(Name = "GetLibraryUsers")]
        public IActionResult GetAllLibraryUsers()
        {
            var LibraryUser = _libraryUserService.GetAllLibraryUsers();

            return Ok(LibraryUser);
        }

        [HttpGet("Id/{id}")]
        public IActionResult GetLibraryUserById(Guid Id)
        {
            return Ok(_libraryUserService.GetLibraryUserById(Id));
        }

        [HttpGet("Name/{name}")]
        public IActionResult GetLibraryUserByName(string name)
        {
            var result = _libraryUserService.GetLibraryUserByName(name);

            if (result is null) 
                return NotFound("Library User not found");

            return Ok(result);
        }

        [AllowAnonymous]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost(Name = "Register")]
        public async Task<IActionResult> Register(UserRegisterDTO model)
        {
            //this method shoould use createAtRoute to return 201 and not 200, following rest pprinciples           
            var result = await _libraryUserService.Register(model);

            if (result.Success)
                return Ok(result.Message);

            return BadRequest(result.Message);
        }
    }
}

using System;
using System.Threading.Tasks;
using AthensLibrary.Data.Interface;
using AthensLibrary.Model.DataTransferObjects.LibraryUserControllerDTO;
using AthensLibrary.Model.Entities;
using AthensLibrary.Model.Enumerators;
using AthensLibrary.Model.Helpers.HelperClasses;
using AthensLibrary.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AthensLibrary.Controllers
{
    [Route("api/LibraryUser")]
    [ApiController]
    public class LibraryUserController : ControllerBase
    {
        private readonly IServiceFactory _serviceFactory;
        public LibraryUserController(IServiceFactory serviceFactory)
        {         
            _serviceFactory = serviceFactory;
        }

        [HttpPost("Register"), Authorize(Policy = "AdminRolePolicy")]
        public async Task<IActionResult> Register(UserRegisterDTO model)
        {
            //this method shoould use createAtRoute to return 201 and not 200, following rest pprinciples
            if (!ModelState.IsValid) return BadRequest("Object sent from client is null.");
            var libraryuserService = _serviceFactory.GetServices<ILibraryUserService>();
            var (success, message) = await libraryuserService.Register(model);
            return success ? Ok(message) : BadRequest(message);
        }
    }
}

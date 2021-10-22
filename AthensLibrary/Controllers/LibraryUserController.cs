using System;
using System.Threading.Tasks;
using AthensLibrary.Data.Interface;
using AthensLibrary.Filters.ActionFilters;
using AthensLibrary.Filters.AuthorizationFilters;
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
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [Route("api/LibraryUser")]
    [ApiController]
    public class LibraryUserController : ControllerBase
    {
        private readonly IServiceFactory _serviceFactory;
        public LibraryUserController(IServiceFactory serviceFactory)
        {         
            _serviceFactory = serviceFactory;
        }

        [HttpPost(Name ="Register")]
        public async Task<IActionResult> Register(UserRegisterDTO model)
        {
            //this method shoould use createAtRoute to return 201 and not 200, following rest pprinciples
            var libraryuserService = _serviceFactory.GetServices<ILibraryUserService>();
            var (success, message) = await libraryuserService.Register(model);
            return success ? Ok(message) : BadRequest(message);
        }
    }
}

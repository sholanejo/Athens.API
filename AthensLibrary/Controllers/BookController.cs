using AthensLibrary.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthensLibrary.Controllers
{

   

    [Route("api/book")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IServiceFactory _serviceFactory;

        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(Guid id)
        {
            _bookService.Delete(id);
            return Ok();
        }
    }

   
}

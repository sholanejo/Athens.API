using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AthensLibrary.Filters.ActionFilters;
using AthensLibrary.Filters.AuthorizationFilters;
using AthensLibrary.Model.DataTransferObjects.BookControllerDTO;
using AthensLibrary.Model.RequestFeatures;
using AthensLibrary.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace AthensLibrary.Controllers
{
    [Route("api/Books")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IServiceFactory _serviceFactory;

        public BookController(IServiceFactory serviceFactory)
        {

            _serviceFactory = serviceFactory;
            _bookService = _serviceFactory.GetServices<IBookService>();
        }

        [HttpPost("{bookId}/CheckOut"), Authorize]
        public async Task<IActionResult> CheckOutABook(Guid bookId)
        {
            var configuration = _serviceFactory.GetServices<IConfiguration>();

            HttpContext.Session.TryGetValue("Email", out byte[] email);

            if (email is null) 
                return NotFound("Cant find a logged in user!");

            var model = new CheckOutABookDTO(configuration) { Email = Encoding.ASCII.GetString(email) };  
            
            var result  = await _bookService.CheckOutABook(bookId, model);

            if (result.Success)
                return Ok(result.Message);

            return BadRequest(result.Message);
        }

        //PUT
        [HttpPut("{borrowDetailId}/returnABook"), Authorize]
        public IActionResult ReturnABook(Guid borrowDetailId)
        //what if i want to return a list of books
        {
            var result = _bookService.ReturnABook(borrowDetailId);

            if (result.Success)
                return Ok(result.Message);

            return BadRequest(result.Message);
        }

        //GET
        [HttpGet(Name = "GetBooks")]
        public IActionResult GetAllBooks()
        {
            BookParameters bookParameters = new BookParameters();
            var result = _bookService.GetAllBooks(bookParameters);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(result.MetaData));

            return Ok(result);
        }

        [HttpGet("booksByAuthor/{authorId}", Name ="GetBooksByAuthor")]
        public IActionResult GetAllBooksByAnAuthor(string authorId, [FromQuery] BookParameters bookParameters)
        {
            var result = _bookService.GetAllBooksByAnAuthor(authorId, bookParameters);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(result.MetaData));

            return Ok(result);
        }

        [HttpGet("booksByLoggedInAuthor", Name = "GetBooksByLoggedInAuthor"), Authorize(Policy = "AuthorRolePolicy")]
        public IActionResult GetAllBooksByLoggedInAuthor([FromQuery] BookParameters bookParameters)
        {
            HttpContext.Session.TryGetValue("Email", out byte[] email);

            var result =_bookService.GetAllBooksByAnAuthor(Encoding.ASCII.GetString(email), bookParameters);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(result.MetaData));

            return Ok(result);            
        }

        [HttpDelete("id/{id}", Name = "GetBookById")]
        public IActionResult Delete(Guid id)
        {
            _bookService.Delete(id);
            return Ok("Delete SUccessful");
        }


        [HttpGet("booksByCategory/{categoryName}", Name = "GetBooksByCategory")]
        public IActionResult GetAllBooksByACategory(string categoryName, [FromQuery] BookParameters bookParameters)
        {
            var result = _bookService.GetAllBooksInACategory(categoryName, bookParameters);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(result.MetaData));

            return Ok(result);
        }


        [HttpGet("booksByYear/{year}", Name = "GetBooksByYearPublished")]
        public IActionResult GetAllBooksByYearPublished(int year, [FromQuery] BookParameters bookParameters)
        {
            var result = _bookService.GetAllBooksPublishedInAYear(year, bookParameters);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(result.MetaData));

            return Ok(result);
        }

        [HttpGet("booksByTitle/{bookTitle}", Name = "GetBooksByTitle")]
        public IActionResult GetAllBooksByTitle(string bookTitle, [FromQuery] BookParameters bookParameters)
        {
            var result = _bookService.GetBooksByTitle(bookTitle, bookParameters);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(result.MetaData));

            return Ok(result);
        }


        [HttpGet("{Id}")]
        public IActionResult GetBookByIsbn(Guid Id) => Ok(_bookService.GetABookByIsbn(Id));

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost(Name = "CreateBook"), MultiplePolicysAuthorize("AdminRolePolicy;AuthorRolePolicy")]
        //What if i want to Create a book, and i am logged in as an author will i still need to provide my AuthorId
        public async Task<IActionResult> CreateBook(BookCreationDTO model)
        {
            //Add createdby, so we can tell authors ceation from Admin's
            if (!ModelState.IsValid) 
                return BadRequest("Object sent from client is null.");

            var bookService = _serviceFactory.GetServices<IBookService>();

            var result = await bookService.CreateBook(model);

            if (result.Success)
                return Ok(result.Message);

            return BadRequest(result.Message);
        }

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost("bookRequest"), MultiplePolicysAuthorize("LibraryUserRolePolicy;AuthorRolePolicy")]
        public async Task<IActionResult> RequestABook(UserBookRequestDTO model)
        {
            if (!ModelState.IsValid) 
                return BadRequest("Object sent from client is null.");

            var booklibService = _serviceFactory.GetServices<IBookService>();

            var result = await booklibService.RequestABook(model);

            if (result.Success)
                return Ok(result.Message);

            return BadRequest(result.Message);
        }

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost("bookDeleteRequest"), Authorize(Policy = "AuthorRolePolicy")]
        public async Task<IActionResult> RequestABookDelete(UserBookDeleteRequestDTO model)
        {
            if (!ModelState.IsValid) 
                return BadRequest("Object sent from client is null.");

            HttpContext.Session.TryGetValue("Email", out byte[] email);

            if (email.Length == 0) 
                return NotFound("Email from last session not found");

            var result = await _bookService.RequestABookDelete(model, Encoding.ASCII.GetString(email));

            if (result.Success)
                return Ok(result.Message);

            return BadRequest(result.Message);
        }

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPatch("id/{Id}"), Authorize(Policy = "AdminRolePolicy")]
        public IActionResult UpdateBook(Guid Id, [FromBody] JsonPatchDocument<BookUpdateDTO> model)
        {
            var result =  _bookService.UpdateBook(Id, model);

            if (result.Success)
                return Ok(result.Message);

            return BadRequest(result.Message);
        }
    }
}

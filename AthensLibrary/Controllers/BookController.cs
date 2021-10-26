using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AthensLibrary.Filters.AuthorizationFilters;
using AthensLibrary.Model.DataTransferObjects.BookControllerDTO;
using AthensLibrary.Model.RequestFeatures;
using AthensLibrary.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
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
            HttpContext.Session.TryGetValue("Email", out byte[] email);
            if (email is null) return NotFound("Cant find a logged in user!");
            var model = new CheckOutABookDTO { Email = Encoding.ASCII.GetString(email) };            
            var (success, message) = await _bookService.CheckOutABook(bookId, model);
            return success ? Ok(message) : BadRequest(message);
        }

        //PUT
        [HttpPut("{borrowDetailId}/ReturnABook"), Authorize]
        public async Task<IActionResult> ReturnABook(Guid borrowDetailId)
        //what if i want to return a list of books
        {
            var (success, message) = await _bookService.ReturnABook(borrowDetailId);
            return success ? Ok(message) : BadRequest(message);
        }

        //GET
        [HttpGet(Name = "GetBooks")]
        public IActionResult GetAllBooks([FromQuery] BookParameters bookParameters)
        {
            var result = _bookService.GetAllBooks(bookParameters);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(result.MetaData));
            return Ok(result);
        }

        [HttpGet("BooksByAuthor/{authorId}")]
        public IActionResult GetAllBooksByAnAuthor(string authorId, [FromQuery] BookParameters bookParameters)
        {
            var result = _bookService.GetAllBooksByAnAuthor(authorId, bookParameters);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(result.MetaData));
            return Ok(result);
        }

        [HttpGet("BooksByLoggedInAuthor"), Authorize(Policy = "AuthorRolePolicy")]
        public IActionResult GetAllBooksByLoggedInAuthor([FromQuery] BookParameters bookParameters)
        {
            HttpContext.Session.TryGetValue("Email", out byte[] email);
            var result =_bookService.GetAllBooksByAnAuthor(Encoding.ASCII.GetString(email), bookParameters);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(result.MetaData));
            return Ok(result);            
        }

        [HttpDelete("id/{id}")]
        public IActionResult Delete(Guid id)
        {

            _bookService.Delete(id);
            return Ok();
        }

        [HttpGet("BooksByCategory/{categoryName}")]
        public IActionResult GetAllBooksByACategory(string categoryName, [FromQuery] BookParameters bookParameters)
        {
            var result = _bookService.GetAllBooksInACategory(categoryName, bookParameters);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(result.MetaData));
            return Ok(result);
        }

        [HttpGet("BooksByYear/{year}")]
        public IActionResult GetAllBooksByYearPublished(int year, [FromQuery] BookParameters bookParameters)
        {
            var result = _bookService.GetAllBooksPublishedInAYear(year, bookParameters);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(result.MetaData));
            return Ok(result);
        }

        [HttpGet("BooksByTitle/{bookTitle}")]
        public IActionResult GetAllBooksByTitle(string bookTitle, [FromQuery] BookParameters bookParameters)
        {
            var result = _bookService.GetBooksByTitle(bookTitle, bookParameters);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(result.MetaData));
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public IActionResult GetBookByIsbn(Guid Id) => Ok(_bookService.GetABookByIsbn(Id));
        
        [HttpPost(Name = "CreateBook"), MultiplePolicysAuthorize("AdminRolePolicy;AuthorRolePolicy")]
        //What if i want to Create a book, and i am logged in as an author will i still need to provide my AuthorId
        public async Task<IActionResult> CreateBook(BookCreationDTO model)
        {
            //Add createdby, so we can tell authors ceation from Admin's
            if (!ModelState.IsValid) return BadRequest("Object sent from client is null.");
            var bookService = _serviceFactory.GetServices<IBookService>();
            var (success, message) = await bookService.CreateBook(model);
            return success ? Ok(message) : BadRequest(message);
        }

        [HttpPost("BookRequest"), MultiplePolicysAuthorize("LibraryUserRolePolicy;AuthorRolePolicy")]
        public async Task<IActionResult> RequestABook(UserBookRequestDTO model)
        {
            if (!ModelState.IsValid) return BadRequest("Object sent from client is null.");
            var booklibService = _serviceFactory.GetServices<IBookService>();
            var (success, message) = await booklibService.RequestABook(model);
            return success ? Ok(message) : BadRequest(message);
        }
        [HttpPost("BookDeleteRequest"), Authorize(Policy = "AuthorRolePolicy")]
        public async Task<IActionResult> RequestABookDelete(UserBookDeleteRequestDTO model)
        {
            if (!ModelState.IsValid) return BadRequest("Object sent from client is null.");
            HttpContext.Session.TryGetValue("Email", out byte[] email);
            if (email.Length == 0) return NotFound("Email from last session not found");
            var (success, message) = await _bookService.RequestABookDelete(model, Encoding.ASCII.GetString(email));
            return success ? Ok(message) : BadRequest(message);
        }

        [HttpPatch("id/{Id}")]
        public async Task<IActionResult> UpdateBook(Guid Id, [FromBody] JsonPatchDocument<BookUpdateDTO> model)
        {
            var (success, message) = await _bookService.UpdateBook(Id, model);
            return success ? Ok(message) : BadRequest(message);
        }
    }
}

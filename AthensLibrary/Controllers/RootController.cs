using System.Collections.Generic;
using AthensLibrary.Model.LinkModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace AthensLibrary.Controllers
{
    [Route("api")]
    [ApiController]
    public class RootController : ControllerBase
    {
        private readonly LinkGenerator _linkGenerator;
        public RootController(LinkGenerator linkGenerator) => _linkGenerator = linkGenerator;

        [HttpGet(Name = "GetRoot")]
        public IActionResult GetRoot([FromHeader(Name = "Accept")] string mediaType)
        {
            if (mediaType.Contains("application/vnd.codemaze.apiroot"))
            {
                var list = new List<Link> {
                    new Link
                    {
                        Href = _linkGenerator.GetUriByName(HttpContext, nameof(GetRoot), new {}),
                        Rel = "self",
                        Method = "GET"
                    },
                    new Link
                    {
                      Href = _linkGenerator.GetUriByName(HttpContext, "Login", new {}),
                      Rel = "Login",
                      Method = "Post"
                    },
                    new Link
                    {
                      Href = _linkGenerator.GetUriByName(HttpContext, "Register", new {}),
                      Rel = "Register",
                      Method = "Post"
                    },
                    new Link 
                    {
                        Href = _linkGenerator.GetUriByName(HttpContext, "GetBooks", new {}),
                        Rel = "Get_All_Books",
                        Method = "Get"
                    },
                    new Link
                    {
                      Href = _linkGenerator.GetUriByName(HttpContext, "CreateBook", new {}),
                      Rel = "Create_A_Book",
                      Method = "Post"
                    },
                    new Link
                    {
                      Href = _linkGenerator.GetUriByName(HttpContext, "AddAuthor", new {}),
                      Rel = "Add_Author",
                      Method = "Post"
                    },
                    new Link
                    {
                      Href = _linkGenerator.GetUriByName(HttpContext, "GetAuthors", new {}),
                      Rel = "Get_All_Authors",
                      Method = "GET"
                    },
                    new Link
                    {
                      Href = _linkGenerator.GetUriByName(HttpContext, "CreateCategory", new {}),
                      Rel = "Create_A_Category",
                      Method = "Post"
                    },
                    new Link
                    {
                      Href = _linkGenerator.GetUriByName(HttpContext, "GetCategories", new {}),
                      Rel = "Get_All_Categories",
                      Method = "GET"
                    },
                };
                return Ok(list);
            }
            return NoContent();
        }

    }
}
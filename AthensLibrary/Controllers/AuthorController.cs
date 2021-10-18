using AthensLibrary.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthensLibrary.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthorService authorService;

        public AuthorController(IAuthorService _authorService)
        {
            this.authorService = _authorService;
        }
        [HttpPost]
        public void Create(string name, string email)
        {
            

        }

        
    }
}

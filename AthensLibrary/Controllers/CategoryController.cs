using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AthensLibrary.Data.Interface;
using AthensLibrary.Filters.ActionFilters;
using AthensLibrary.Model.DataTransferObjects.CategoryControllerDTO;
using AthensLibrary.Service.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace AthensLibrary.Controllers
{
    [Authorize(Policy = "AdminRolePolicy")]
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {        
        private readonly ICategoryService _categoryService;
        private readonly IServiceFactory _serviceFactory;

        public CategoryController(IUnitOfWork unitofWork, IServiceFactory serviceFactory, ICategoryService categoryService)
        {           
            _serviceFactory = serviceFactory;
            _categoryService = categoryService;
        }

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost(Name = "CreateCategory")]
        public IActionResult CreateCategory([FromBody] CategoryCreationDTO category)
        {
            //Check category does not alreadt exist!!!!
           // if (!ModelState.IsValid) return BadRequest("Object sent from client is null.");
            var result = _categoryService.AddCategory(category);

            if (result.Success)
                return Ok(result.Message);

            return BadRequest(result.Message);
        }

        [HttpGet("Id/{id}")]
        public IActionResult GetCategoryById(Guid Id)
        {
            return Ok(_categoryService.GetCategoryById(Id));
        }        

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost("categoryCollection")]
        public IActionResult CreateCategoryCollection([FromBody] IEnumerable<CategoryCreationDTO> model)
        {
            foreach (var category in model)
            {
                _categoryService.AddCategory(category);
            } 
            
            return Ok("Categories Created Successfully");
        }

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPatch("id/{Id}")]
        public IActionResult UpdateCategory(Guid id, [FromBody] JsonPatchDocument<CategoryCreationDTO> model)
        {
            var result = _categoryService.UpdateCategory(id, model);

            if (result.Success) 
                return Ok(result.Message);

            return BadRequest(result. Message);
        }

        [HttpDelete("id/{id}")]
        public IActionResult Delete(Guid id)
        {
            _categoryService.Delete(id);  
            
            return Ok();
        }

        [AllowAnonymous]
        [HttpGet("categoryName/{name}")]
        public IActionResult GetCategoryByName(string name)
        {
            if (name == null) 
                return BadRequest("Please input a valid category name");

            var category = _categoryService.GetCategoryByName(name); 
            
            return Ok(category);
        }

        [AllowAnonymous]
        [HttpGet(Name = "GetCategories")]
        public IActionResult GetAllCategories()
        {
            return Ok(_categoryService.GetCategories());
        }
    }
}

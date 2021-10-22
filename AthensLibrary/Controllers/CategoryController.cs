using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AthensLibrary.Data.Interface;
using AthensLibrary.Model.DataTransferObjects.CategoryControllerDTO;
using AthensLibrary.Service.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AthensLibrary.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitofWork;
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;
        private readonly IServiceFactory _serviceFactory;

        public CategoryController(IUnitOfWork unitofWork, IMapper mapper, IServiceFactory serviceFactory, ICategoryService categoryService)
        {
            _unitofWork = unitofWork;
            _mapper = mapper;
            _serviceFactory = serviceFactory;
            _categoryService = categoryService;
        }

        [HttpPost(Name = "CreateCategory"), Authorize(Policy = "AdminRolePolicy")]   
        public async Task<IActionResult> CreateCategory([FromBody]CategoryCreationDTO category)
        {
            if (!ModelState.IsValid) return BadRequest("Object sent from client is null.");
            var (success, message) = await _categoryService.AddCategory(category);
            return success ? Ok(message) : BadRequest(message);
        }

        [HttpGet("Id/{id}"), Authorize(Policy = "AdminRolePolicy")]
        public IActionResult GetCategoryById(Guid Id)
        {
            return Ok(_categoryService.GetCategoryById(Id));
        }

        [HttpGet("categoryName/{name}")]
        public IActionResult GetCategoryByName(string name)
        {
            if (name == null) return BadRequest("Please input a valid category name");
            var category = _categoryService.GetCategoryByName(name);
            var categoryDto = _mapper.Map<CategoryDto>(category);
            return Ok(categoryDto);
        }

        [HttpGet(Name ="GetCategories")]
        public IActionResult GetAllCategories()
        {
             return Ok(_categoryService.GetCategories());
        }

        [HttpPost("categoryCollection")]
        public async Task<IActionResult> CreateCategoryCollection([FromBody] IEnumerable<CategoryCreationDTO> model)
        {
            var categoryEntities = _mapper.Map<IEnumerable<CategoryCreationDTO>>(model);
            foreach (var category in categoryEntities)
            {
                await _categoryService.AddCategory(category);
            }
            await _unitofWork.SaveChangesAsync();
            return Ok("Categories Created Successfully");
        }
        [HttpDelete("delete/{id}")]
        public IActionResult  Delete(Guid id)
        {
            _categoryService.Delete(id);
            _unitofWork.SaveChanges();
            return Ok();
        }
    }
}

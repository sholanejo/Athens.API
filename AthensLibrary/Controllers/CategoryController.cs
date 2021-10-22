using AthensLibrary.Data.Interface;
using AthensLibrary.Model.DataTransferObjects.CategoryControllerDTO;
using AthensLibrary.Model.Entities;
using AthensLibrary.Service.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        [HttpPost("addcategory")]
        public async Task<IActionResult> CreateCategory([FromBody]CategoryCreationDTO category)
        {
            if (!ModelState.IsValid) return BadRequest("Object sent from client is null.");
            var (success, message) = await _categoryService.AddCategory(category);
            return success ? Ok(message) : BadRequest(message);
        }

        [HttpGet("Id/{id}"), Authorize(Policy = "AdminRolePolicy")]
        public IActionResult GetCategoryById(Guid Id)
        {
            var category = _categoryService.GetCategoryById(Id);
            return Ok(category);
        }

        [HttpGet("categoryname/{name}")]
        public IActionResult GetCategoryByName(string name)
        {
            if (name == null) return BadRequest("Please input a valid category name");
            var category = _categoryService.GetCategoryByName(name);
            var categoryDto = _mapper.Map<CategoryDto>(category);
            return Ok(categoryDto);
        }

        [HttpGet("allcategories")]
        public IActionResult GetAllCategories()
        {
            var category = _categoryService.GetCategories();
            return Ok(category);
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
            return Ok("categories created successfully");
        }

        public void Delete(Guid id)
        {
            _categoryService.SoftDelete(id);
            _unitofWork.SaveChanges();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AthensLibrary.Data.Interface;
using AthensLibrary.Model.DataTransferObjects.CategoryControllerDTO;
using AthensLibrary.Model.Entities;
using AthensLibrary.Service.Interface;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;

namespace AthensLibrary.Service.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitofWork;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IServiceFactory _serviceFactory;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitofWork, IServiceFactory serviceFactory, IMapper mapper)
        {
            _unitofWork = unitofWork;
            _categoryRepository = unitofWork.GetRepository<Category>();
            _serviceFactory = serviceFactory;
            _mapper = mapper;
        }

        public Task<IEnumerable<Book>> GetAllBooksByCategory()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CategoryDto> GetCategories()
        {
            var allCategories = _categoryRepository.GetAll();
            var categoryDto = _mapper.Map<IEnumerable<CategoryDto>>(allCategories);
            return categoryDto;
        }

        public CategoryDto GetCategoryById(Guid id)
        {
            var category = _categoryRepository.GetById(id);
            var categoryDto = _mapper.Map<CategoryDto>(category);
            return categoryDto;
        }

        public Category GetCategoryByName(string name)
        {
            var category = _categoryRepository.GetSingleByCondition(c => c.CategoryName == name);
            return category;
        }

        public async Task<(bool, string)> AddCategory(CategoryCreationDTO category)
        {
            var categoryEntity = _mapper.Map<Category>(category);
            await _categoryRepository.AddAsync(categoryEntity);
            return (await _unitofWork.SaveChangesAsync()) < 1 ? (false, "Internal Db error") : (true, "Category Created successfully");
        }

        public void Delete(Guid id)
        {
            _categoryRepository.SoftDelete(id);
            _unitofWork.SaveChanges();
        }

        public async Task<(bool, string)> UpdateCategory(Guid categoryId, JsonPatchDocument<CategoryCreationDTO> model)
        {
            var categoryEntity = _categoryRepository.GetById(categoryId);
            if (categoryEntity is null) return (false, $"Book with Id {categoryId} not found");
            categoryEntity.UpdatedAt = DateTime.Now;
            var categoryToPatch = _mapper.Map<CategoryCreationDTO>(categoryEntity);
            model.ApplyTo(categoryToPatch);
            _mapper.Map(categoryToPatch, categoryEntity);
            return (await _unitofWork.SaveChangesAsync()) < 1 ? (false, "Internal Db error, Update failed") : (true, "Category update successfully");
        }
    }
}

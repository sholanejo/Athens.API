using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AthensLibrary.Data.Interface;
using AthensLibrary.Model.DataTransferObjects.CategoryControllerDTO;
using AthensLibrary.Model.Entities;
using AthensLibrary.Model.Helpers.HelperClasses;
using AthensLibrary.Service.Interface;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;

namespace AthensLibrary.Service.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IServiceFactory _serviceFactory;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitofWork, IServiceFactory serviceFactory, IMapper mapper)
        {
            _unitOfWork = unitofWork;
            _categoryRepository = unitofWork.GetRepository<Category>();
            _serviceFactory = serviceFactory;
            _mapper = mapper;
        }
       
        public IEnumerable<CategoryDto> GetCategories()
        {
            var allCategories = _categoryRepository.GetAll();
            return _mapper.Map<IEnumerable<CategoryDto>>(allCategories);            
        }

        public CategoryDto GetCategoryById(Guid id)
        {
            var category = _categoryRepository.GetById(id);

            return _mapper.Map<CategoryDto>(category);           
        }

        public Category GetCategoryByName(string name) =>
             _categoryRepository.GetSingleByCondition(c => c.CategoryName == name);           
       

        public ReturnModel AddCategory(CategoryCreationDTO category)
        {
            var categoryEntity = _mapper.Map<Category>(category);

            _categoryRepository.Add(categoryEntity);

            return new ReturnModel { Success = false, Message = "Category created successfully" };
        }        

        public ReturnModel Delete(Guid id) =>
             _categoryRepository.SoftDelete(id);                  

        public ReturnModel UpdateCategory(Guid categoryId, JsonPatchDocument<CategoryCreationDTO> model)
        {
            var categoryEntity = _categoryRepository.GetById(categoryId);

            if (categoryEntity is null)
                return new ReturnModel {Success = false, Message = $"Book with Id {categoryId} not found"};

            categoryEntity.UpdatedAt = DateTime.Now;

            var categoryToPatch = _mapper.Map<CategoryCreationDTO>(categoryEntity);

            model.ApplyTo(categoryToPatch);

            _mapper.Map(categoryToPatch, categoryEntity);

            return new ReturnModel { Success = true, Message = "Category update successfully" };
        }       
    }
}

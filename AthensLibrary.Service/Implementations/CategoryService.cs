using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AthensLibrary.Data.Interface;
using AthensLibrary.Model.Entities;
using AthensLibrary.Service.Interface;

namespace AthensLibrary.Service.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IServiceFactory _serviceFactory;

        public CategoryService(IUnitofWork unitofWork,IServiceFactory serviceFactory)
        {
            _unitofWork = unitofWork;
            _categoryRepository = unitofWork.GetRepository<Category>();
            _serviceFactory = serviceFactory;

        }

        public Task<IEnumerable<Book>> GetAllBooksByCategory()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> GetCategories()
        {
            return _categoryRepository.GetAll().ToList();
        }

        public Category GetCategoryById(Guid id)
        {
            var category = _categoryRepository.GetById(id);
            return category;
        }

        public Category GetCategoryByName(string name)
        {
            var category = _categoryRepository.GetSingleByCondition(c => c.CategoryName == name);
            return category;
        }

        public void AddCategory(Category category)
        {
            _categoryRepository.Insert(category);
        }
    }
}

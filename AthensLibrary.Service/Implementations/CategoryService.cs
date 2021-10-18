using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AthensLibrary.Model.Entities;
using AthensLibrary.Service.Interface;

namespace AthensLibrary.Service.Implementations
{
    public class CategoryService : ICategoryService
    {
        public Task<IEnumerable<Book>> GetAllBooksByCategory()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Category>> GetCategories()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Category>> GetCategoryById()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Category>> GetCategoryByName()
        {
            throw new NotImplementedException();
        }
    }
}

using AthensLibrary.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthensLibrary.Service.Interface
{
    public interface ICategoryService
    {
        Task<IEnumerable<Book>> GetAllBooksByCategory();
        IEnumerable<Category> GetCategories();
        Category GetCategoryByName(string name);
        Category GetCategoryById(Guid id);
        void AddCategory(Category category);
    }
}

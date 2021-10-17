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
        Task<IEnumerable<Category>> GetCategories();
        Task<IEnumerable<Category>> GetCategoryByName();
        Task<IEnumerable<Category>> GetCategoryById();
    }
}

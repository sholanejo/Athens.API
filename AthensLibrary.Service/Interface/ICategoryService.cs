using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AthensLibrary.Model.DataTransferObjects.CategoryControllerDTO;
using AthensLibrary.Model.Entities;
using AthensLibrary.Model.Helpers.HelperClasses;
using Microsoft.AspNetCore.JsonPatch;

namespace AthensLibrary.Service.Interface
{
    public interface ICategoryService
    {
        ReturnModel UpdateCategory(Guid categoryId, JsonPatchDocument<CategoryCreationDTO> model);
        IEnumerable<CategoryDto> GetCategories();
        Category GetCategoryByName(string name);
        CategoryDto GetCategoryById(Guid id);
        Task<ReturnModel> AddCategory(CategoryCreationDTO category);
        ReturnModel Delete(Guid id);
    }
}

using System.Threading.Tasks;
using AthensLibrary.Model.DataTransferObjects.LibraryUserControllerDTO;
using AthensLibrary.Model.Helpers.HelperClasses;
using Microsoft.AspNetCore.JsonPatch;

namespace AthensLibrary.Service.Interface
{
    public interface IUserService 
    {
        Task<ReturnModel> EnrollAuthor(UserRegisterDTO model);
        Task<ReturnModel> UpdateUser(string identifier, JsonPatchDocument<UserUpdateDTO> model);       
    }
}

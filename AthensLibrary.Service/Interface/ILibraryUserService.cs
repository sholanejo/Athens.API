using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AthensLibrary.Model.Entities;
using System.Threading.Tasks;
using AthensLibrary.Model.DataTransferObjects.BookControllerDTO;
using AthensLibrary.Model.DataTransferObjects.LibraryUserControllerDTO;
using AthensLibrary.Model.Helpers.HelperClasses;

namespace AthensLibrary.Service.Interface
{
    public interface ILibraryUserService
    {        
        Task<ReturnModel> Register(UserRegisterDTO model);
        ReturnModel Delete(Guid id);
        LibraryUserDTO GetLibraryUserById(Guid id);
        LibraryUserDTO GetLibraryUserByName(string name);
        IEnumerable<LibraryUser> GetAllLibraryUsers();
    }
}

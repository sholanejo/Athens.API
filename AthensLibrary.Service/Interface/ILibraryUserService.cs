using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AthensLibrary.Model.DataTransferObjects.BookControllerDTO;
using AthensLibrary.Model.DataTransferObjects.LibraryUserControllerDTO;

namespace AthensLibrary.Service.Interface
{
    public interface ILibraryUserService
    {        
        Task<(bool success, string msg)> Register(UserRegisterDTO model);
        Task<(bool success, string msg)> RequestABook(UserBookRequestDTO model);
        Task<(bool success, string msg)> RequestABookDelete(UserBookDeleteRequestDTO model, string email);

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AthensLibrary.Model.DataTransferObjects.LibraryUserControllerDTO;

namespace AthensLibrary.Data.Interface
{
    public interface IAuthentication
    {
        Task<bool> ValidateUser(UserLoginDTO userForAuth); 
        Task<string> CreateToken();
    }
}

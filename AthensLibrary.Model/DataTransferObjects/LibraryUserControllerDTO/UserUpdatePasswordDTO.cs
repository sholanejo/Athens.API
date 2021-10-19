using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthensLibrary.Model.DataTransferObjects.LibraryUserControllerDTO
{
    public class UserUpdatePasswordDTO
    {
        [Required(ErrorMessage = "To update password, please provide the a new password!!!")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "To update password, please provide the old password!!!")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }        
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthensLibrary.Model.DataTransferObjects.LibraryUserControllerDTO
{
    public class UserUpdatePhoneNumberDTO
    {
        [Required(ErrorMessage = "To update your a phone number please provide a new email!!!")]
        [Phone(ErrorMessage = "Invalid Phone Number")]
        public string NewPhoneNumber { get; set; }
    }
}

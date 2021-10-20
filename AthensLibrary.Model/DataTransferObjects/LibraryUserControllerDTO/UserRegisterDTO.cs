using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AthensLibrary.Model.Enumerators;
using AthensLibrary.Model.Helpers.HelperClasses;

namespace AthensLibrary.Model.DataTransferObjects.LibraryUserControllerDTO
{
    public class UserRegisterDTO 
    {
        [Required(ErrorMessage ="Name Required")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Phonenumber required")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Email required")]
        [EmailAddress]
        public string Email { get; set; }        
        public Gender Gender { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime DateOfBirth { get; set; }   
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

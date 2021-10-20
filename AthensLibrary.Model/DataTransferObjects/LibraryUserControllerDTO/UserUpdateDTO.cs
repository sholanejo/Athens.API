using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AthensLibrary.Model.Enumerators;

namespace AthensLibrary.Model.DataTransferObjects.LibraryUserControllerDTO
{
    public class UserUpdateDTO
    { 
        [MinLength(5)][MaxLength(24)]
        public string FullName {get; set;} 
        [Phone]
        public string PhoneNumber {get; set;}   
        [EmailAddress]
        public string Email { get; set; }
        [Range(typeof(Gender), "1", "3", ErrorMessage = "Gender must be a value from 1-3\n1:Male\n2:Female\n3:PreferNotToSay")]
        public Gender Gender { get; set; } 
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

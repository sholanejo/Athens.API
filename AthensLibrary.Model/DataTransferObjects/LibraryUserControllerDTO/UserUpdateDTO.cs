using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AthensLibrary.Model.Enumerators;

namespace AthensLibrary.Model.DataTransferObjects.LibraryUserControllerDTO
{
    public class UserUpdateDTO
    { 
        public string FullName {get; set;}        
        public string PhoneNumber {get; set;}        
        public string Email { get; set; }
        public Gender Gender { get; set; }       
        public string Password { get; set; }
    }
}

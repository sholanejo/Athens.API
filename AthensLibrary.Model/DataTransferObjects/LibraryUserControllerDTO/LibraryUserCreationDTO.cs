using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AthensLibrary.Model.Enumerators;

namespace AthensLibrary.Model.DataTransferObjects.LibraryUserControllerDTO
{
    class LibraryUserCreationDTO
    {  
        public Guid BorrowerId { get; set; }
        public Guid UserId { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}

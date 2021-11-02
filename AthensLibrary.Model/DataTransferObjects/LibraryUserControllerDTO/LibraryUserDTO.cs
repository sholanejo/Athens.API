using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthensLibrary.Model.DataTransferObjects.LibraryUserControllerDTO
{
    public class LibraryUserDTO
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public bool IsDeleted { get; set; }
    }
}

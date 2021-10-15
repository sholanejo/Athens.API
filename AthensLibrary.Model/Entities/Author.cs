using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthensLibrary.Model.Entities
{
    public class Author
    {
        public Guid Id { get; set; } 
        public bool IsActive { get; set; }
        public Guid UserId { get; set; }
        public bool IsDeleted { get; set; }

    }
}

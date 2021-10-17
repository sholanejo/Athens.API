using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AthensLibrary.Model.Helpers.HelperClasses;
using AthensLibrary.Model.Helpers.HelperInterfaces;

namespace AthensLibrary.Model.Entities
{
    public class Category : TimeStamp, ISoftDelete
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; }
        public bool IsDeleted { get; set; }
    }
}

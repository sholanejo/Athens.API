using AthensLibrary.Model.Helpers.HelperInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthensLibrary.Model.Helpers.HelperClasses
{
    public class SoftDelete : ISoftDelete
    {
        public bool IsDeleted { get; set ; }
    }
}

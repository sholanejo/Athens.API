using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AthensLibrary.Model.Enumerators;
using AthensLibrary.Model.Helpers.HelperClasses;
using Microsoft.AspNetCore.Identity;

namespace AthensLibrary.Model.Entities
{
    public class User : UserTimeStamp
    {       
        public string FullName { get; set; }
        public Gender Gender { get; set; }       
        public string UpdatedBy { get; set; }
        public string CreatedBy { get; set; }
    }
}

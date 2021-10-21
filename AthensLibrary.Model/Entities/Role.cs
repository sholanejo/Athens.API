using System;
using AthensLibrary.Model.Helpers;
using AthensLibrary.Model.Helpers.HelperClasses;
using AthensLibrary.Model.Helpers.HelperInterfaces;
using Microsoft.AspNetCore.Identity;

namespace AthensLibrary.Model.Entities
{
    public class Role : RoleTimeStamp , ISoftDelete
    {
        public string UpdatedBy { get; set; }
        public string CreatedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}

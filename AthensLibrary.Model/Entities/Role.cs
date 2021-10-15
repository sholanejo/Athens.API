using System;
using AthensLibrary.Model.Helpers;
using AthensLibrary.Model.Helpers.HelperClasses;
using Microsoft.AspNetCore.Identity;

namespace AthensLibrary.Model.Entities
{
    public class Role : RoleTimeStamp
    {
        public string UpdatedBy { get; set; }
        public string CreatedBy { get; set; }
    }
}

using System;
using AthensLibrary.Model.Helpers.HelperInterfaces;
using Microsoft.AspNetCore.Identity;

namespace AthensLibrary.Model.Helpers.HelperClasses
{
    public class UserTimeStamp : IdentityUser, ISoftDelete
    {
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set ; }
        public DateTime UpdatedAt { get; set; }
    }
}

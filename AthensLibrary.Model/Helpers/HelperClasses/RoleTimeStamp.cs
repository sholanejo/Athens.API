using System;
using Microsoft.AspNetCore.Identity;

namespace AthensLibrary.Model.Helpers.HelperClasses
{
    public class RoleTimeStamp : IdentityRole
    {
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }
    }
}

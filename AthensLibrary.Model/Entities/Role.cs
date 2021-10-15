using System;
using Microsoft.AspNetCore.Identity;

namespace AthensLibrary.Model.Entities
{
    public class Role : IdentityRole
    {
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public string CreatedBy { get; set; }
    }
}

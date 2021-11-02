using System;
using AthensLibrary.Model.Helpers.HelperInterfaces;

namespace AthensLibrary.Model.Entities
{
    public class LibraryUser : ISoftDelete
    {        
        public Guid Id { get; set; }       
        public string UserId { get; set; }   
        public bool IsDeleted { get; set; }     
        public User User { get; set; }
    }
}

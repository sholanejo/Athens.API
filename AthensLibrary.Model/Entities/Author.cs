using System;
using AthensLibrary.Model.Helpers.HelperInterfaces;

namespace AthensLibrary.Model.Entities
{
    public class Author : ISoftDelete
    {
        public Guid Id { get; set; }        
        //since everyone should have is Deleted add it to timestamp 
        public bool IsDeleted { get; set; }       
        public string UserId { get ; set ; }
        public User User { get; set; }
    }
}

using AthensLibrary.Model.Helpers.HelperInterfaces;
using System;

namespace AthensLibrary.Model.DataTransferObjects
{
    public class AuthorDTO 
    {
        public Guid Id { get; set; }        
        public string UserId { get; set; }       
        public bool IsDeleted { get; set; }
    }
}

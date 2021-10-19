using System;
using AthensLibrary.Model.Enumerators;
using AthensLibrary.Model.Helpers.HelperInterfaces;

namespace AthensLibrary.Model.Entities
{
    public class LibraryUser : IReader
    {
        //IReader shows who is a reader, it contains BorrowerId 
        public Guid Id { get; set; }
        public string BorrowerId { get; set; }
        public string UserId { get; set; }   
       
        
    }
}

using System;
using AthensLibrary.Model.Enumerators;
using AthensLibrary.Model.Helpers.HelperInterfaces;

namespace AthensLibrary.Model.Entities
{
    public class LibraryUser : IReader
    {
        //IReader shows who is a reader, it contains BorrowerId 
        public Guid Id { get; set; }
        public Guid BorrowerId { get; set; }
        public Guid UserId { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }        
    }
}

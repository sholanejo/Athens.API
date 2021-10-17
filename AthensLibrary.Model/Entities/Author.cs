using System;
using AthensLibrary.Model.Helpers.HelperInterfaces;

namespace AthensLibrary.Model.Entities
{
    public class Author : IReader, ISoftDelete
    {
        public Guid Id { get; set; } 
        public bool IsActive { get; set; }
        public Guid UserId { get; set; }
        public Guid BorrowerId { get; set; }
        public bool IsDeleted { get; set; }

    }
}

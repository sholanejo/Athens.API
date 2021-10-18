using System;
using AthensLibrary.Model.Helpers.HelperInterfaces;

namespace AthensLibrary.Model.Entities
{
    public class Author : IReader
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
        public string UserId { get; set; }
        public Guid BorrowerId { get; set; }
        public bool IsDeleted { get; set; }

    }
}

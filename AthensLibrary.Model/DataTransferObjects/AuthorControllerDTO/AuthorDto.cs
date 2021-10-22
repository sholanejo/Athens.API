using AthensLibrary.Model.Helpers.HelperInterfaces;
using System;

namespace AthensLibrary.Model.DataTransferObjects
{
    public class AuthorDTO : IReader
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
        public string UserId { get; set; }
        public string BorrowerId { get; set; }
        public bool IsDeleted { get; set; }
    }
}

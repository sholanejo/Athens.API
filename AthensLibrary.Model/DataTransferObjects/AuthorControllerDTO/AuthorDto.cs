using AthensLibrary.Model.Entities;
using System;

namespace AthensLibrary.Model.DataTransferObjects
{
    public class AuthorDTO
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
        public string UserId { get; set; }
        public Guid BorrowerId { get; set; }
        public bool IsDeleted { get; set; }
    }
}

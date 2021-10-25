using System;

namespace AthensLibrary.Model.DataTransferObjects.BookControllerDTO
{
    public class BookUpdateDTO
    {
        public string Title { get; set; }
        public DateTime PublicationYear { get; set; }
        public string CategoryName { get; set; }
        public int InitialBookCount { get; set; }
        public int CurrentBookCount { get; set; }
        public Guid AuthorId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}

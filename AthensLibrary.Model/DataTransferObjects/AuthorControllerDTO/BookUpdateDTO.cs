using System;
using System.ComponentModel.DataAnnotations;

namespace AthensLibrary.Model.DataTransferObjects.AuthorControllerDTO
{
    public class BookUpdateDTO
    {
        [Required(ErrorMessage = "Book Title is required")]
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

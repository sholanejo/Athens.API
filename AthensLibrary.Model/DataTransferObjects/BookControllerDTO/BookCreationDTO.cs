using System;
using System.ComponentModel.DataAnnotations;

namespace AthensLibrary.Model.DataTransferObjects.BookControllerDTO
{
    public class BookCreationDTO
    {
        [Required(ErrorMessage = "Book title is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Year published is required")]
        public DateTime PublicationYear { get; set; }
        [Required(ErrorMessage = "Category name is required")]
        public string CategoryName { get; set; }
        [Required(ErrorMessage = "Initial book count is required")]
        public int InitialBookCount { get; set; }
        public int CurrentBookCount { get; set; }
        [Required(ErrorMessage = "Author Id required is required")]
        public Guid AuthorId { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}

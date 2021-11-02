using System;
using System.ComponentModel.DataAnnotations;
using AthensLibrary.Model.Enumerators;

namespace AthensLibrary.Model.DataTransferObjects.BookControllerDTO
{
    public class UserBookRequestDTO
    {
        [Required(ErrorMessage = "Please provide the book title you are requesting")]
        public string BookTitle { get; set; }
        [MinLength(5)]
        [MaxLength(24)]
        [Required(ErrorMessage = "Please provide the Author of the book you are requesting")]
        public string AuthorName { get; set; }
        
       // [Range(typeof(RequestType), "1", "2", ErrorMessage = "Please select the type of type of book request you want")]
        public RequestType RequestType { get; set; }
    }
}

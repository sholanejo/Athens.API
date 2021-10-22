using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthensLibrary.Model.DataTransferObjects.BookControllerDTO
{
    public class UserBookDeleteRequestDTO
    {
        [Required(ErrorMessage = "Please provide the book title you are requesting")]
        [MinLength(5)]
        [MaxLength(24)]
        public string BookTitle { get; set; }
    }
}

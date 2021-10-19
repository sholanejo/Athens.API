using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthensLibrary.Model.DataTransferObjects.LibraryUserControllerDTO
{
    public class CheckOutABookDTO
    {
        public Guid Id { get; set; }
        public Guid BookId { get; set; }        
        public DateTime BorrowedOn { get; set; } = DateTime.Now;
        public DateTime DueDate { get; set; } = DateTime.Now.AddDays(15); 
        public DateTime? ReturnDate { get; set; } = null;  //nullable because it wil be populated whenever the user retuens the book
      
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace AthensLibrary.Model.DataTransferObjects.BookControllerDTO
{
    public class CheckOutABookDTO
    {   
        /*[Required(ErrorMessage= " Bood iD required")]
        public string BorrowerId { get; set; }    */    
        public string Email { get; set; }        
        public DateTime BorrowedOn { get; set; } = DateTime.Now;
        public DateTime DueDate { get; set; } = DateTime.Now.AddDays(15);        
      
    }
}

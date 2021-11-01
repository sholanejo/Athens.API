using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Configuration;

namespace AthensLibrary.Model.DataTransferObjects.BookControllerDTO
{
    public class CheckOutABookDTO
    {
        private readonly IConfiguration _configuration;
        
        public CheckOutABookDTO(IConfiguration configuration) 
        {
            _configuration = configuration;
            BorrowedOn = DateTime.Now;
            DueDate = DateTime.Now.AddDays(Convert.ToDouble(_configuration["DueDate"]));            
        }

        public string Email { get; set; }        
        public DateTime BorrowedOn { get; set; } 
        public DateTime DueDate { get; set; }       
      
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using AthensLibrary.Model.Helpers.HelperClasses;

namespace AthensLibrary.Model.DataTransferObjects.CategoryControllerDTO
{
    public class CategoryCreationDTO : TimeStamp
    {
        [Required(ErrorMessage="CategoryName is required")]
        public string CategoryName { get; set; }
        public bool IsDeleted { get; set; } = false;    
        
    }
}

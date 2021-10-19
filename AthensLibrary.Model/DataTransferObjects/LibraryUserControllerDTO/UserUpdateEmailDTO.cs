using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthensLibrary.Model.DataTransferObjects.LibraryUserControllerDTO
{
    public class UserUpdateEmailDTO
    {
        [Required(ErrorMessage="To update an email please provide a new email!!!")]
        [EmailAddress]
        public string NewEmail { get; set; }
    }
}

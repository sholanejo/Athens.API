using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthensLibrary.Model.Entities
{
    public class BorrowDetail
    {
        public Guid Id { get; set; }
        public Guid BookId { get; set; }
        public Guid BorrowerId { get; set; }
        public DateTime BorrowedOn { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}

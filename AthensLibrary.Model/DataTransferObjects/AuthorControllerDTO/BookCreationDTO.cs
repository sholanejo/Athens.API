using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthensLibrary.Model.DataTransferObjects.AuthorControllerDTO
{
    public class BookCreationDTO
    {
        public string Title { get; set; }
        public DateTime PublicationYear { get; set; }
        public string CategoryName { get; set; }
        public int InitialBookCount { get; set; }
        public int CurrentBookCount { get; set; }
        public Guid AuthorId { get; set; }
        public bool IsDeleted { get; set; }
    }
}

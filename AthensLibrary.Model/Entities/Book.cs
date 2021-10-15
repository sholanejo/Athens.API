using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AthensLibrary.Model.Enumerators;

namespace AthensLibrary.Model.Entities
{
    public class Book
    {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public DateTime PublicationYear { get; set; }
        public Category Category { get; set; }
        public int InitialBookCount { get; set; }
        public int CurrentBookCount { get; set; }
        public Guid AuthorId { get; set; }
        public bool IsDeleted { get; set; }
    }
}

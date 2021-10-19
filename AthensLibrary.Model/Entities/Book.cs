using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AthensLibrary.Model.Enumerators;
using AthensLibrary.Model.Helpers.HelperClasses;
using AthensLibrary.Model.Helpers.HelperInterfaces;

namespace AthensLibrary.Model.Entities
{
    public class Book : TimeStamp,ISoftDelete
    {
        //Timestamp holds things like createdAt and Updated At
        public Guid ID { get; set; }
        public string Title { get; set; }
        public DateTime PublicationYear { get; set; }
        public string CategoryName { get; set; }
        public int InitialBookCount { get; set; }
        public int CurrentBookCount { get; set; }
        public Guid AuthorId { get; set; }
        public bool IsDeleted { get; set; }
    }
}

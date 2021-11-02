using AthensLibrary.Model.Helpers.HelperClasses;
using AthensLibrary.Model.Helpers.HelperInterfaces;
using System;

namespace AthensLibrary.Model.Entities
{
    public class Book : TimeStamp, ISoftDelete
    {
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

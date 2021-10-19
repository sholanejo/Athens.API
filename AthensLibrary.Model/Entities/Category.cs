using System;
using AthensLibrary.Model.Helpers.HelperClasses;
using AthensLibrary.Model.Helpers.HelperInterfaces;

namespace AthensLibrary.Model.Entities
{
    public class Category : TimeStamp, ISoftDelete
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; }
        public bool IsDeleted { get; set; }
    }
}

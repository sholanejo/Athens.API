using System;
using AthensLibrary.Model.Helpers.HelperClasses;

namespace AthensLibrary.Model.Entities
{
    public class Category : TimeStamp
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; }
    }
}

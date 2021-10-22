using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AthensLibrary.Model.Enumerators;
using AthensLibrary.Model.Helpers.HelperClasses;

namespace AthensLibrary.Model.Entities
{
    public class UserBookRequest : UserRequest
    {
        public string BookTitle { get; set; }
        public string AuthorName { get; set; }
        public string RequestType { get; set; }
    }
}

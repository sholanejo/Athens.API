using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AthensLibrary.Model.Enumerators;

namespace AthensLibrary.Model.Helpers.HelperClasses
{
    public abstract class UserRequest
    {        
        public Guid Id { get; set; }
        public RequestStatus RequestStatus { get; set; } = RequestStatus.Pending;
        public bool IsActive { get; set; } = true;
    }
}

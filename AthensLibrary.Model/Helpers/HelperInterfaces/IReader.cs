using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthensLibrary.Model.Helpers.HelperInterfaces
{
    public interface IReader
    {
        Guid BorrowerId { get; set; }
        Guid UserId { get; set; }
    }
}

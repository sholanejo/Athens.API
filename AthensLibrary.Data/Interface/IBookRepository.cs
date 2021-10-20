using AthensLibrary.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthensLibrary.Data.Interface
{
    public interface IBookRepository : IRepository<Book>
    {
        void Delete(Guid Id);
    }
}

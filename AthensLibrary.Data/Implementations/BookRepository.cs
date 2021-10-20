using AthensLibrary.Data.Interface;
using AthensLibrary.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthensLibrary.Data.Implementations
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public void Delete(Guid Id)
        {
            throw new NotImplementedException();
        }
    }
}

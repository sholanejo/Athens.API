using AthensLibrary.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthensLibrary.Data.Interface
{
    interface IAuthorRepository : IRepository<Author>
    {
        void Delete(Guid Id);
    }

}

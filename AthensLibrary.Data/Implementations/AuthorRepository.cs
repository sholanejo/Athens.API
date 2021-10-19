using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AthensLibrary.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace AthensLibrary.Data.Implementations
{
    public class AuthorRepository : Repository<Author>
    {
        private readonly AthensDbContext context;

        public AuthorRepository(DbContext context) : base(context)
        {
            //this.context = context;
        }

        public void run(Guid AuthorId)
        {
            context.Authors.Include(c => c.User).ToList();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AthensLibrary.Data.Interface;
using AthensLibrary.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace AthensLibrary.Data.Implementations
{
    public class AuthorRepository : Repository<Author>,IAuthorRepository
    {
        

        public AuthorRepository(AthensDbContext context) : base(context)
        {
            //this.context = context;
        }

        public void run(Guid AuthorId)
        {
            this._dbContext.Authors.Include(c => c.User).ToList();
        }

        public void Delete(Guid Id)
        {
            var author = _dbSet.Find(Id);
            this._dbContext.Entry(author).State = EntityState.Modified;
            this._dbContext.SaveChanges();

        }
    }
}

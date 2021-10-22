using AthensLibrary.Model.Helpers.HelperInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AthensLibrary.Model.Helpers.HelperInterfaces;

namespace AthensLibrary.Data.Interface
{
        public interface IUnitOfWork
        {
            IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, ISoftDelete;
            int SaveChanges();
            Task<int> SaveChangesAsync();
        }
        public interface IUnitofWork<TContext> : IUnitOfWork
        {
        }
   
}

using AthensLibrary.Data.Interface;
using AthensLibrary.Model.Entities;
using AthensLibrary.Model.Helpers.HelperInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AthensLibrary.Data.Implementations
{
    public class Repository<T> : IRepository<T> where T : class, ISoftDelete
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<T> _dbSet;
        private Expression<Func<T, bool>> softDeletePredicate = e=>e.IsDeleted == false;


        public Repository(DbContext context)
        {
            _dbContext = context;
            _dbSet = _dbContext.Set<T>();
        }

        public T Add(T obj)
        {
            _dbContext.Add(obj);
            return obj;
        }

        public void Insert(T entity)
        {
            _dbSet.Add(entity);
        }

        public IQueryable<T> GetAllWithInclude(string incPpt)
        {
            return _dbContext.Set<T>().Include(incPpt);           
        }

        public async Task<T> AddAsync(T obj)
        {
            Add(obj);
            await _dbContext.SaveChangesAsync();
            return obj;
        }
        public IEnumerable<T> GetAll()
        {
            // return only none deleted items
            return _dbSet.Where(softDeletePredicate).ToList();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {

            return await _dbSet.Where(softDeletePredicate).ToListAsync();

        }

        public IEnumerable<T> GetByCondition(Expression<Func<T, bool>> predicate = null, Func<IQueryable, IOrderedQueryable> orderby = null, int? skip = null, int? take = null, params string[] includeProperties)
        {
            if (predicate is null) return _dbSet.Where(softDeletePredicate).ToList();
            return _dbSet.Where(softDeletePredicate).Where(predicate);
        }

        public T GetById(object id)
        {
           
            var entity = _dbSet.Find(id);
            // should we return null when object is deleted
           // if (entity.IsDeleted == true) return null;    
            return entity;
        }

        public async Task<T> GetByIdAsync(object id)
        {
            var entity = await _dbSet.FindAsync(id);
            // should we return null when object is deleted
            // if (entity.IsDeleted == true) return null;
            return entity;
        }

        public T GetSingleByCondition(Expression<Func<T, bool>> predicate = null, Func<IQueryable, IOrderedQueryable> orderby = null, params string[] includeProperties)
        {
            if (predicate is null) return _dbSet.Where(softDeletePredicate).ToList().FirstOrDefault();
            return  _dbSet.Where(softDeletePredicate).Where(predicate).FirstOrDefault();
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate is null) return await _dbSet.Where(softDeletePredicate).AnyAsync();
            return await _dbSet.Where(softDeletePredicate).AnyAsync(predicate);
        }

        public bool Any(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate is null) return _dbSet.Where(softDeletePredicate).Any();
            return _dbSet.Where(softDeletePredicate).Any(predicate);
        }

        public void SoftDelete(Guid Id)
        {
            var entity = _dbSet.Find(Id);
            entity.IsDeleted = true;
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

    }
}

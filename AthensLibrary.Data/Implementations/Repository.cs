using AthensLibrary.Data.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AthensLibrary.Data.Implementations
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private bool disposedValue = false;
        private readonly DbContext _dbContext;
        private readonly DbSet<T> _dbSet;
        public Repository(DbContext context)
        {
            _dbContext = context ?? throw new ArgumentException(nameof(context));
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
            return _dbContext.Set<T>().AsQueryable().Include(incPpt);
        }

        public async Task<T> AddAsync(T obj)
        {
            Add(obj);
            await _dbContext.SaveChangesAsync();
            return obj;
        }



        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();

        }

        public IEnumerable<T> GetByCondition(Expression<Func<T, bool>> predicate = null, Func<IQueryable, IOrderedQueryable> orderby = null, int? skip = null, int? take = null, params string[] includeProperties)
        {
            if (predicate is null) return _dbSet.ToList();
            return _dbSet.Where(predicate);
        }

        public T GetById(object id)
        {
            return _dbSet.Find(id);
        }

        public async Task<T> GetByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public T GetSingleByCondition(Expression<Func<T, bool>> predicate = null, Func<IQueryable, IOrderedQueryable> orderby = null, params string[] includeProperties)
        {
            if (predicate is null) return _dbSet.ToList().FirstOrDefault();
            return _dbSet.Where(predicate).FirstOrDefault();
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate is null) return await _dbSet.AnyAsync();
            return await _dbSet.AnyAsync(predicate);
        }

        public bool Any(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate is null) return _dbSet.Any();
            return _dbSet.Any(predicate);
        }
    }
}

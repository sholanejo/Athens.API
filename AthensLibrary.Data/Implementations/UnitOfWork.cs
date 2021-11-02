﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AthensLibrary.Data.Interface;
using AthensLibrary.Model.Helpers.HelperInterfaces;
using Microsoft.EntityFrameworkCore;

namespace AthensLibrary.Data.Implementations
{
    public class UnitofWork<TContext> : IUnitofWork<DbContext> where TContext : DbContext
    {
        private Dictionary<Type, object> _repositories;
        private readonly TContext _context;
        public UnitofWork(TContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, ISoftDelete
        {
            if (_repositories == null) _repositories = new Dictionary<Type, object>();

            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type)) _repositories[type] = new Repository<TEntity>(_context);
            return (IRepository<TEntity>)_repositories[type];
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}

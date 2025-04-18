using AuthServer.Core.Repositories;
using AuthServer.Data.Context;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore; // ✅ EF Core üçündür

using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks; 

namespace AuthServer.Data.Repositories
{

    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();

        }
        public Task AddAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public TEntity Update(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}

using InvoiceXml.Application.Contracts.Persistence;
using InvoiceXml.Domain.Common;
using InvoiceXml.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceXml.Infrastructure.Repositories
{
    public class RepositoryBase<T> : IAsyncRepo<T> where T : EntityBase
    {
        protected readonly DataContext _dbContext;

        public RepositoryBase(DataContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<T> AddAsync(T entity)
        {
            entity.CreatedDate = DateTime.Now;
            _dbContext.Set<T>().Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            entity.LastModifiedDate = DateTime.Now;
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (predicate != null) query = query.Where(predicate);

            return await query.ToListAsync();
        }
    }
}

using InvoiceXml.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceXml.Application.Contracts.Persistence
{
    public interface IAsyncRepo<T> where T : EntityBase
    {
        Task<T> GetByIdAsync(int id);
        
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);

        Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> predicate);
    }
}

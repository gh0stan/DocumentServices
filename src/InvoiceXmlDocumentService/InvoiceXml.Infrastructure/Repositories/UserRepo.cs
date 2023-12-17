using InvoiceXml.Application.Contracts.Persistence;
using InvoiceXml.Domain.Models;
using InvoiceXml.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceXml.Infrastructure.Repositories
{
    public class UserRepo : RepositoryBase<User>, IUserRepo
    {
        public UserRepo(DataContext dbContext) : base(dbContext)
        {

        }

        public async Task<IEnumerable<User>> GetAllUsersForAbonentAsync(int abonentId)
        {
            return await _dbContext.Users.Where(u => u.AbonentId == abonentId).ToListAsync();
        }
    }
}

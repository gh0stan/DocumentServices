using InvoiceXml.Domain.Models;

namespace InvoiceXml.Application.Contracts.Persistence
{
    public interface IUserRepo : IAsyncRepo<User>
    {
        Task<IEnumerable<User>> GetAllUsersForAbonentAsync(int abonentId);
    }
}

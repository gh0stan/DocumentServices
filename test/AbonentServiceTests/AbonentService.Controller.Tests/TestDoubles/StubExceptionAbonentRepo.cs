using AbonentService.Data.Models;
using AbonentService.Interfaces;

namespace AbonentService.Api.Tests.TestDoubles;

public class StubExceptionAbonentRepo : IAbonentRepo
{
    public bool SaveChanges()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Abonent> GetAllAbonents()
    {
        throw new NotImplementedException();
    }

    public Abonent GetAbonentById(int Id)
    {
        throw new NotImplementedException();
    }

    public void CreateAbonent(Abonent abonent)
    {
        throw new NotImplementedException();
    }
}
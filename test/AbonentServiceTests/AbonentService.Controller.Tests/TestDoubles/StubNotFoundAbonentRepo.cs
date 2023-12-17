using AbonentService.Data.Models;
using AbonentService.Interfaces;

namespace AbonentService.Api.Tests;

public class StubNotFoundAbonentRepo : IAbonentRepo
{
    public void CreateAbonent(Abonent abonent)
    {
        throw new NotImplementedException();
    }

    public Abonent GetAbonentById(int Id)
    {
        return null;
    }

    public IEnumerable<Abonent> GetAllAbonents()
    {
        return null;
    }

    public bool SaveChanges()
    {
        throw new NotImplementedException();
    }
}
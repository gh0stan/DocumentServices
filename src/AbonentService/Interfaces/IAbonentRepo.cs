using AbonentService.Data.Models;

namespace AbonentService.Interfaces
{
    public interface IAbonentRepo
    {
        bool SaveChanges();
        IEnumerable<Abonent> GetAllAbonents();
        Abonent GetAbonentById(int Id);
        void CreateAbonent(Abonent abonent);
    }
}

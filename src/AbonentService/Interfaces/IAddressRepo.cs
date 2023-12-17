using AbonentService.Data.Models;

namespace AbonentService.Interfaces
{
    public interface IAddressRepo
    {
        bool SaveChanges();
        Address GetAddressById(int Id);
        void CreateAddress(Address address);
        IEnumerable<Address> GetAllAddressesByAbonentId(int abonentId);

    }
}

using AbonentService.Data.Models;
using AbonentService.Interfaces;

namespace AbonentService.Data.Repositories
{
    public class AddressRepo : IAddressRepo
    {
        private readonly DataContext _context;

        public AddressRepo(DataContext context)
        {
            _context = context;
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void CreateAddress(Address address)
        {
            if (address == null) 
            { 
                throw new ArgumentNullException(nameof(address));
            }

            _context.Addresses.Add(address);
        }

        public IEnumerable<Address> GetAllAddressesByAbonentId(int abonentId)
        {
            return _context.Addresses.Where(a => a.AbonentId == abonentId).ToList();
        }

        public Address GetAddressById(int id)
        {
            return _context.Addresses.FirstOrDefault(a => a.Id == id);
        }
    }
}

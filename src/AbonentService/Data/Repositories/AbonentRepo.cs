using AbonentService.Data.Models;
using AbonentService.Interfaces;

namespace AbonentService.Data.Repositories
{
    public class AbonentRepo : IAbonentRepo
    {
        private readonly DataContext _context;

        public AbonentRepo(DataContext context)
        {
            _context = context;
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void CreateAbonent(Abonent abonent)
        {
            if (abonent == null) 
            { 
                throw new ArgumentNullException(nameof(abonent));
            }

            if (_context.Abonents.Any(a => a.Name == abonent.Name)) 
            { 
                throw new ArgumentException($"A company with that name ({abonent.Name}) already exists.");
            }

            _context.Abonents.Add(abonent);
        }

        public Abonent GetAbonentById(int id)
        {
            return _context.Abonents.SingleOrDefault(a => a.Id == id);
        }

        public IEnumerable<Abonent> GetAllAbonents()
        {
            return _context.Abonents.ToList();
        }

    }
}

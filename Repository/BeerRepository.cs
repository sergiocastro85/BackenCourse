using Microsoft.EntityFrameworkCore;
using WebApplicationBackend.Models;

namespace WebApplicationBackend.Repository
{
    public class BeerRepository : IRepository<Beer>
    {   

        private StoreContext _context;

        public BeerRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Beer>> GetAsync()
            =>await _context.Beers.ToListAsync();

        public async Task<Beer> GetById(int id)
            => await _context.Beers.FindAsync(id);


           
        public void UpdateAsync(Beer beer)
        {
            _context.Attach(beer);
            _context.Beers.Entry(beer).State = EntityState.Modified;
        }

        public void DeleteAsync(Beer beer)
            => _context.Beers.Remove(beer);


        public async Task Save()
            => await _context.SaveChangesAsync();

        public async Task Add(Beer beer)
             => await _context.Beers.AddAsync(beer);

        public IEnumerable<Beer> Search(Func<Beer, bool> filter)
            => _context.Beers.Where(filter).ToList();
    }
}

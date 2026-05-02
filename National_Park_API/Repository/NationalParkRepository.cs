using National_Park_API.Data;
using National_Park_API.Models;
using National_Park_API.Repository.IRepository;

namespace National_Park_API.Repository
{
    public class NationalParkRepository : INationalParkRepository
    {
        private readonly ApplicationDbContext _context;

        public NationalParkRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public ICollection<National_Park> GetNationalParks()
        {
            return _context.NationalParks.ToList();
        }

        public National_Park GetNationalPark(int nationalParkId)
        {
            return _context.NationalParks.Find(nationalParkId);
        }

        public bool NationalParkExists(int nationalParkId)
        {
            return _context.NationalParks.Any(np => np.Id == nationalParkId);
        }

        public bool NationalParkExists(string nationalParkName)
        {
            return _context.NationalParks.Any(np => np.Name == nationalParkName);
        }

        public bool CreateNationalPark(National_Park nationalPark)
        {
            _context.NationalParks.Add(nationalPark);
            return Save();
        }

        public bool UpdateNationalPark(National_Park nationalPark)
        {
            _context.NationalParks.Update(nationalPark);
            return Save();
        }

        public bool DeleteNationalPark(National_Park nationalPark)
        {
            _context.NationalParks.Remove(nationalPark);
            return Save();
        }

        public bool Save()
        {
            return _context.SaveChanges() >0;
        }
    }
}
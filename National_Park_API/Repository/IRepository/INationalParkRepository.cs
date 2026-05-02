using National_Park_API.Models;

namespace National_Park_API.Repository.IRepository
{
    public interface INationalParkRepository
    {
        ICollection <National_Park> GetNationalParks();
        National_Park GetNationalPark(int nationalParkId);
        bool NationalParkExists(int nationalParkId);
        bool NationalParkExists(string nationalparkname);
        bool CreateNationalPark(National_Park nationalPark);
        bool UpdateNationalPark(National_Park nationalPark);
        bool DeleteNationalPark(National_Park nationalPark);    
        bool Save();
    }
}

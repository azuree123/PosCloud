using System.Collections.Generic;
using System.Threading.Tasks;
using POSApp.Core.Models;

namespace POSApp.Core.Repositories
{
    public interface ILocationRepository
    {
        IEnumerable<Location> GetLocations();
        Location GetLocationById(int id);
        void AddLocation(Location location);
        void UpdateLocation(int id, Location location);
        void DeleteLocation(int id);
        IEnumerable<Location> GetApiLocations();
        Task<IEnumerable<Location>> GetLocationsAsync();
        Task<Location> GetLocationByIdAsync(int id);
        Task AddLocationAsync(Location location);
    }
}

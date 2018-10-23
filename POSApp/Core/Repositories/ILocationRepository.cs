using System.Collections.Generic;
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
    }
}

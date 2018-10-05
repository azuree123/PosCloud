using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoSCloudApp.Core.Models;

namespace PoSCloudApp.Core.Repositories
{
    public interface ILocationRepository
    {
        IEnumerable<Location> GetLocations();
        Location GetLocationById(int id);
        void AddLocation(Location location);
        void UpdateLocation(int id, Location location);
        void DeleteLocation(int id);
    }
}

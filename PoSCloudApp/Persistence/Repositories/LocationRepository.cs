using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using PoSCloud.Persistence;
using PoSCloudApp.Core.Models;
using PoSCloudApp.Core.Repositories;

namespace PoSCloudApp.Persistence.Repositories
{
    public class LocationRepository:ILocationRepository
    {
        private PosDbContext _context;

        public LocationRepository(PosDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Location> GetLocations()
        {
            return _context.Locations.ToList();
        }

        public Location GetLocationById(int id)
        {
            return _context.Locations.Find(id);
        }

        public void AddLocation(Location location)
        {
            _context.Locations.Add(location);
        }

        public void UpdateLocation(int id, Location location)
        {
            _context.Locations.Attach(location);
            _context.Entry(location).State = EntityState.Modified;
        }

        public void DeleteLocation(int id)
        {
            var location = new Location { Id = id };
            _context.Locations.Attach(location);
            _context.Entry(location).State = EntityState.Deleted;
        }
    }
}
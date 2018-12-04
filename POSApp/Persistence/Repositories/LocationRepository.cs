using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using POSApp.Core.Models;
using POSApp.Core.Repositories;

namespace POSApp.Persistence.Repositories
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
            return _context.Locations.Where(a=>a.IsActive).ToList();
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
            var location = _context.Locations.FirstOrDefault(a => a.Id == id );
            location.IsActive = false;
            _context.Locations.Attach(location);
            _context.Entry(location).State = EntityState.Modified;
        }
        public IEnumerable<Location> GetApiLocations()
        {
            IEnumerable<Location> locations = _context.Locations.Where(a => !a.Synced).ToList();
            foreach (var location in locations)
            {
                location.Synced = true;
                location.SyncedOn = DateTime.Now;
            }

            _context.SaveChanges();
            return locations;
        }
    }
}
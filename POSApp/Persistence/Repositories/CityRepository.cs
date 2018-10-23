using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using POSApp.Core.Models;
using POSApp.Core.Repositories;

namespace POSApp.Persistence.Repositories
{
    public class CityRepository:ICityRepository
    {
        private PosDbContext _context;

        public CityRepository(PosDbContext context)
        {
            _context = context;
        }
        public IEnumerable<City> GetCities()
        {
            return _context.Cities.Include(a=>a.State).ToList();
        }
        public IEnumerable<City> GetCities(int stateId)
        {
            return _context.Cities.Where(a => a.StateId == stateId).Include(a => a.State).ToList();
        }

        public City GetCity(int id)
        {
            return _context.Cities.Find(id);
        }

        public void AddCity(City city)
        {
            _context.Cities.Add(city);
        }

        public void UpdateCity(int id, City city)
        {
            _context.Cities.Attach(city);
            _context.Entry(city).State = EntityState.Modified;
        }

        public void DeleteCity(int id)
        {
            var city=new City{Id = id};
            _context.Cities.Attach(city);
            _context.Entry(city).State = EntityState.Deleted;
        }
        public IEnumerable<City> GetApiCities()
        {
            IEnumerable<City> cities = _context.Cities.Where(a => !a.Synced).ToList();
            foreach (var city in cities)
            {
                city.Synced = true;
                city.SyncedOn = DateTime.Now;
            }

            _context.SaveChanges();
            return cities;
        }
    }
}
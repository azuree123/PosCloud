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
            return _context.Cities.Include(a=>a.State).Where(a=> !a.IsDisabled).ToList();
        }
        public IEnumerable<City> GetCities(int stateId)
        {
            return _context.Cities.Where(a => a.StateId == stateId && !a.IsDisabled).Include(a => a.State).ToList();
        }

        public City GetCity(int id)
        {
            return _context.Cities.Find(id);
        }

        public void AddCity(City city)
        {
            var inDb = _context.Cities.FirstOrDefault(a => a.Name == city.Name && a.StateId == city.StateId);
            if (inDb == null)
            {
                _context.Cities.Add(city);
            }
            else
            {
                if (inDb.IsDisabled)
                {
                    city.Id = inDb.Id;
                    _context.Entry(inDb).CurrentValues.SetValues(city);
                    _context.Entry(inDb).State = EntityState.Modified;
                }
                else
                {
                    throw new Exception("Entity Already Exists!");
                }
            }
        }

        public void UpdateCity(int id, City city)
        {
            _context.Cities.Attach(city);
            _context.Entry(city).State = EntityState.Modified;
        }

        public void DeleteCity(int id)
        {
            var city = _context.Cities.FirstOrDefault(a => a.Id == id);
            city.IsDisabled = true;
            _context.Cities.Attach(city);
            _context.Entry(city).State = EntityState.Modified;
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
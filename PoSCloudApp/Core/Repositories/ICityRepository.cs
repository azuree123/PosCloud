using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoSCloudApp.Core.Models;

namespace PoSCloudApp.Core.Repositories
{
    public interface ICityRepository
    {
        IEnumerable<City> GetCities(int stateId);
        City GetCity(int id);
        void AddCity(City city);
        void UpdateCity(int id, City city);
        void DeleteCity(int id);
        IEnumerable<City> GetCities();
    }
}

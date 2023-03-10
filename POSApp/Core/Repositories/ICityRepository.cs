using System.Collections.Generic;
using System.Threading.Tasks;
using POSApp.Core.Models;

namespace POSApp.Core.Repositories
{
    public interface ICityRepository
    {
        IEnumerable<City> GetCities(int stateId);
        City GetCity(int id);
        void AddCity(City city);
        void UpdateCity(int id, City city);
        void DeleteCity(int id);
        IEnumerable<City> GetCities();
        IEnumerable<City> GetApiCities();
        Task<IEnumerable<City>> GetCitiesAsync();
        Task AddCityAsync(City city);
        Task<City> GetCityAsync(int id);
    }
}

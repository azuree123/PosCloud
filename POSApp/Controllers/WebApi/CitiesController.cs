using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using POSApp.Core;
using POSApp.Core.Models;
using POSApp.Core.ViewModels;
using POSApp.Core.ViewModels.Sync;

namespace POSApp.Controllers.WebApi
{
    public class CitiesController : ApiController
    {
        private IUnitOfWork _unitOfWork;
        public CitiesController()
        {
            
        }
        public CitiesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IHttpActionResult> GetCities()
        {
            
            return Ok(Mapper.Map<CityModelView[]>( await  _unitOfWork.CityRepository.GetCitiesAsync()));
        }

        // GET: api/CitiesSync/5
        public async Task<IHttpActionResult> GetCity(int id)
        {
            return Ok(await _unitOfWork.CityRepository.GetCityAsync(id));
        }

        // POST: api/CitiesSync
        public async Task<IHttpActionResult> AddCities([FromBody]SyncObject sync)
        {
            try
            {
                List<City> cities = System.Web.Helpers.Json.Decode<List<City>>(sync.Object);
                foreach (var city in cities)
                {
                    city.Code = city.Id.ToString();
                    city.Synced = true;
                    city.SyncedOn = DateTime.Now;
                    await _unitOfWork.CityRepository.AddCityAsync(city);
                }

                if (!await _unitOfWork.CompleteAsync())
                {
                    throw new Exception("Error Occured While Adding");
                }
                return Ok("Success");
            }
            catch (Exception e)
            {
                return Ok("Error");
                throw;
            }
        }

        // PUT: api/CitiesSync/5
        public void UpdateCity(int id, [FromBody]string value)
        {
        }

        // DELETE: api/CitiesSync/5
        public void DeleteCity(int id)
        {
        }
    }
}

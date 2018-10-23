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
            return Ok(Mapper.Map<CityModelView[]>(_unitOfWork.CityRepository.GetApiCities()));
        }

        // GET: api/CitiesSync/5
        public async Task<IHttpActionResult> GetCity(int id)
        {
            return Ok(_unitOfWork.CityRepository.GetCity(id));
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
                    _unitOfWork.CityRepository.AddCity(city);
                }
                _unitOfWork.Complete();
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

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
    public class LocationsController : ApiController
    {

        private IUnitOfWork _unitOfWork;
        public LocationsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IHttpActionResult> GetLocations()
        {
            return Ok(Mapper.Map<LocationModelView[]>(_unitOfWork.LocationRepository.GetApiLocations()));
        }

        // GET: api/LocationsSync/5
        public async Task<IHttpActionResult> GetLocation(int id, int storeId)
        {
            return Ok(_unitOfWork.LocationRepository.GetLocationById(id));
        }

        // POST: api/LocationsSync
        public async Task<IHttpActionResult> AddLocations([FromBody]SyncObject sync)
        {
            try
            {
                List<Location> locations = System.Web.Helpers.Json.Decode<List<Location>>(sync.Object);
                foreach (var location in locations)
                {
                    location.Code = location.Id.ToString();
                    location.Synced = true;
                    location.SyncedOn = DateTime.Now;
                    _unitOfWork.LocationRepository.AddLocation(location);
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

        // PUT: api/LocationsSync/5
        public void UpdateLocation(int id, [FromBody]string value)
        {
        }

        // DELETE: api/LocationsSync/5
        public void DeleteLocation(int id)
        {
        }
    }
}
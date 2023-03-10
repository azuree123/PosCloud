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
            return Ok(Mapper.Map<LocationModelView[]>(await _unitOfWork.LocationRepository.GetLocationsAsync()));
        }

        // GET: api/LocationsSync/5
        public async Task<IHttpActionResult> GetLocation(int id, int storeId)
        {
            return Ok(await _unitOfWork.LocationRepository.GetLocationByIdAsync(id));
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
                    await _unitOfWork.LocationRepository.AddLocationAsync(location);
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
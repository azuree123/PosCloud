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
using POSApp.Models;
using POSApp.Persistence;

namespace POSApp.Controllers.WebApi
{
    public class UsersController : ApiController
    {
        private IUnitOfWork _unitOfWork;

        public UsersController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: api/Users

        // GET: api/Users

        public async Task<IHttpActionResult> GetUsers(int storeId, bool forceFull, int deviceId)
        {
            var data = new object();
            if (forceFull)
            {
                data = await _unitOfWork.UserRepository.GetApiUsersAsync(storeId);


                return Ok(Mapper.Map<UserViewModel[]>(data));

            }
            else
            {

                var lastSync =
                    await _unitOfWork.IncrementalSyncronizationRepository.GetLastIncrementalSyncronization(storeId,
                        deviceId, "Users");
                if (lastSync == null)
                {
                    data = await _unitOfWork.UserRepository.GetApiUsersAsync(storeId);
                }
                else
                {
                    data = await _unitOfWork.UserRepository.GetAllUsersAsyncIncremental(storeId,
                        lastSync.LastSynced);
                }
                _unitOfWork.IncrementalSyncronizationRepository.AddIncrementalSyncronization(new IncrementalSyncronization()
                {
                    StoreId = storeId,
                    DeviceId = deviceId,
                    LastSynced = DateTime.Now,
                    TableName = "Users"

                });
                _unitOfWork.Complete();
                return Ok(Mapper.Map<UserViewModel[]>(data));


            }


        }

        // GET: api/Users/5
        public async Task<IHttpActionResult> GetUser(string id, int storeId)
        {
            return Ok(await _unitOfWork.UserRepository.GetUserByIdAsync(id, storeId));
        }

        // POST: api/Users
        public async Task<IHttpActionResult> AddUser([FromBody]SyncObject sync)
        {
            try
            {
                //List<User> users = System.Web.Helpers.Json.Decode<List<User>>(sync.Object);
                //foreach (var user in users)
                //{
                //    user.Code = user.Id.ToString();
                //    user.Synced = true;
                //    user.SyncedOn = DateTime.Now;
                //    _unitOfWork.UserRepository.AddUser(user);
                //}
                //_unitOfWork.Complete();
                return Ok(1);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT: api/Users/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Users/5
        public void Delete(int id)
        {
        }
    }
}
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
    public class ModifiersController : ApiController
    {
        private IUnitOfWork _unitOfWork;
        public ModifiersController()
        {

        }
        public ModifiersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: api/Modifiers

        public async Task<IHttpActionResult> GetModifiers(int storeId, bool forceFull, int deviceId)
        {
            var data = new object();
            if (forceFull)
            {
                data = await _unitOfWork.ModifierRepository.GetModifiersAsync(storeId);


                return Ok(Mapper.Map<ModifierViewModel[]>(data));

            }
            else
            {

                var lastSync =
                    await _unitOfWork.IncrementalSyncronizationRepository.GetLastIncrementalSyncronization(storeId,
                        deviceId, "Modifiers");
                if (lastSync == null)
                {
                    data = await _unitOfWork.ModifierRepository.GetModifiersAsync(storeId);
                }
                else
                {
                    data = await _unitOfWork.ModifierRepository.GetAllModifiersAsyncIncremental(storeId,
                        lastSync.LastSynced);
                }
                _unitOfWork.IncrementalSyncronizationRepository.AddIncrementalSyncronization(new IncrementalSyncronization()
                {
                    StoreId = storeId,
                    DeviceId = deviceId,
                    LastSynced = DateTime.Now,
                    TableName = "Modifiers"

                });
                _unitOfWork.Complete();
                return Ok(Mapper.Map<ModifierViewModel[]>(data));


            }


        }
        //public async Task<IHttpActionResult> GetModifiers(int storeId)
        //{
        //    return Ok(await _unitOfWork.ModifierRepository.GetModifiersAsync(storeId));
        //}

        // GET: api/ModifiersSync/5
        public async Task<IHttpActionResult> GetModifierById(int id, int storeId)
        {
            return Ok(await _unitOfWork.ModifierRepository.GetModifierByIdAsync(id, storeId));
        }

        // POST: api/ModifiersSync
        public async Task<IHttpActionResult> AddModifiers([FromBody]SyncObject sync)
        {
            try
            {
                List<Modifier> modifiers = System.Web.Helpers.Json.Decode<List<Modifier>>(sync.Object);
                foreach (var modifier in modifiers)
                {
                    modifier.Code = modifier.Id.ToString();
                    modifier.Synced = true;
                    modifier.SyncedOn = DateTime.Now;
                    await _unitOfWork.ModifierRepository.AddModifierAsync(modifier);
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

        // PUT: api/ModifiersSync/5
        public void UpdateModifier(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ModifiersSync/5
        public void DeleteModifier(int id)
        {
        }
    }
}
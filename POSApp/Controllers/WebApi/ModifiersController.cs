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
        public async Task<IHttpActionResult> GetModifiers(int storeId)
        {
            return Ok(_unitOfWork.ModifierRepository.GetModifiers(storeId));
        }

        // GET: api/ModifiersSync/5
        public async Task<IHttpActionResult> GetModifierById(int id, int storeId)
        {
            return Ok(_unitOfWork.ModifierRepository.GetModifierById(id, storeId));
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
                    _unitOfWork.ModifierRepository.AddModifier(modifier);
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
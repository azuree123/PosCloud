﻿using System;
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
    public class UnitsController : ApiController
    {
        private IUnitOfWork _unitOfWork;
        public UnitsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: api/Units
        public async Task<IHttpActionResult> GetUnits(int storeId)
        {
            return Ok(Mapper.Map<UnitViewModel[]>(_unitOfWork.UnitRepository.GetUnit(storeId)));
        }

        // GET: api/UnitCategoriesSync/5
        public async Task<IHttpActionResult> GetUnit(int id, int storeId)
        {
            return Ok(_unitOfWork.UnitRepository.GetUnitById(id, storeId));
        }

        // POST: api/UnitCategoriesSync
        public async Task<IHttpActionResult> AddUnits([FromBody]SyncObject sync)
        {
            try
            {
                List<Unit> unites = System.Web.Helpers.Json.Decode<List<Unit>>(sync.Object);
                foreach (var unit in unites)
                {
                    unit.Code = unit.Id.ToString();
                    unit.Synced = true;
                    unit.SyncedOn = DateTime.Now;
                    _unitOfWork.UnitRepository.AddUnit(unit);
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

        // PUT: api/UnitCategoriesSync/5
        public void UpdateUnit(int id, [FromBody]string value)
        {
        }

        // DELETE: api/UnitCategoriesSync/5
        public void DeleteUnit(int id)
        {
        }
    }
}
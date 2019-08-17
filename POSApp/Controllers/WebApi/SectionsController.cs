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
    public class SectionsController : ApiController
    {
        private IUnitOfWork _unitOfWork;
        public SectionsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: api/ProductCategoryGroups
        //public async Task<IHttpActionResult> GetSections(int storeId)
        //{
        //    return Ok(Mapper.Map<SectionViewModel[]>(await _unitOfWork.SectionRepository.GetSectionsAsync(storeId)));
        //}

        // GET: api/ProductCategoryGroupCategoriesSync/5
        public async Task<IHttpActionResult> GetSection(int id, int storeId)
        {
            return Ok(await _unitOfWork.SectionRepository.GetSectionByIdAsync(id, storeId));
        }
        public async Task<IHttpActionResult> GetSections(int storeId, bool forceFull, int deviceId)
        {
            var data = new object();
            if (forceFull)
            {
                data = await _unitOfWork.SectionRepository.GetSectionsAsync(storeId);


                return Ok(Mapper.Map<SectionViewModel[]>(data));

            }
            else
            {

                var lastSync =
                    await _unitOfWork.IncrementalSyncronizationRepository.GetLastIncrementalSyncronization(storeId,
                        deviceId, "Sections");
                if (lastSync == null)
                {
                    data = await _unitOfWork.SectionRepository.GetSectionsAsync(storeId);
                }
                else
                {
                    data = await _unitOfWork.SectionRepository.GetAllSectionsAsyncIncremental(storeId,
                        lastSync.LastSynced);
                }
                _unitOfWork.IncrementalSyncronizationRepository.AddIncrementalSyncronization(new IncrementalSyncronization()
                {
                    StoreId = storeId,
                    DeviceId = deviceId,
                    LastSynced = DateTime.Now,
                    TableName = "Sections"

                });
                _unitOfWork.Complete();
                return Ok(Mapper.Map<SectionViewModel[]>(data));


            }


        }
        // POST: api/ProductCategoryGroupCategoriesSync
        public async Task<IHttpActionResult> AddSections([FromBody]SyncObject sync)
        {
            try
            {
                List<Section>sections = System.Web.Helpers.Json.Decode<List<Section>>(sync.Object);
                foreach (var section in sections)
                {
                   section.Code =section.SectionId.ToString();
                   section.Synced = true;
                    section.SyncedOn = DateTime.Now;
                    await _unitOfWork.SectionRepository.AddSectionAsync(section);
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

        // PUT: api/ProductCategoryGroupCategoriesSync/5
        public void UpdateSection(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ProductCategoryGroupCategoriesSync/5
        public void DeleteSection(int id)
        {
        }
    }
}

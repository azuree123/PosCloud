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
        public async Task<IHttpActionResult> GetSections(int storeId)
        {
            return Ok(Mapper.Map<SectionViewModel[]>(_unitOfWork.SectionRepository.GetSections(storeId)));
        }

        // GET: api/ProductCategoryGroupCategoriesSync/5
        public async Task<IHttpActionResult> GetSection(int id, int storeId)
        {
            return Ok(_unitOfWork.SectionRepository.GetSectionById(id, storeId));
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
                    _unitOfWork.SectionRepository.AddSection(section);
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

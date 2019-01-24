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
    public class DepartmentsController : ApiController
    {

        private IUnitOfWork _unitOfWork;
        public DepartmentsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IHttpActionResult> GetDepartments(int storeId)
        {
            return Ok(Mapper.Map<DepartmentViewModel[]>(await _unitOfWork.DepartmentRepository.GetDepartmentsAsync(storeId)));
        }

        // GET: api/DepartmentsSync/5
        public async Task<IHttpActionResult> GetDepartment(int id, int storeId)
        {
            return Ok(await _unitOfWork.DepartmentRepository.GetDepartmentByIdAsync(id,storeId));
        }

        // POST: api/DepartmentsSync
        public async Task<IHttpActionResult> AddDepartments([FromBody]SyncObject sync)
        {
            try
            {
                List<Department> departments = System.Web.Helpers.Json.Decode<List<Department>>(sync.Object);
                foreach (var department in departments)
                {
                    department.Code = department.Id.ToString();
                    department.Synced = true;
                    department.SyncedOn = DateTime.Now;
                    await _unitOfWork.DepartmentRepository.AddDepartmentAsync(department);
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

        // PUT: api/DepartmentsSync/5
        public void UpdateDepartment(int id, [FromBody]string value)
        {
        }

        // DELETE: api/DepartmentsSync/5
        public void DeleteDepartment(int id)
        {
        }
    }
}
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
    public class EmployeesController : ApiController
    {

        private IUnitOfWork _unitOfWork;
        public EmployeesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IHttpActionResult> GetEmployees(int storeId, bool forceFull, int deviceId)
        {
            var data = new object();
            if (forceFull)
            {
                data = await _unitOfWork.EmployeeRepository.GetEmployeesAsync(storeId);


                return Ok(Mapper.Map<EmployeeModelView[]>(data));

            }
            else
            {

                var lastSync =
                    await _unitOfWork.IncrementalSyncronizationRepository.GetLastIncrementalSyncronization(storeId,
                        deviceId, "Employees");
                if (lastSync == null)
                {
                    data = await _unitOfWork.EmployeeRepository.GetEmployeesAsync(storeId);
                }
                else
                {
                    data = await _unitOfWork.EmployeeRepository.GetAllEmployeesAsyncIncremental(storeId,
                        lastSync.LastSynced);
                }
                _unitOfWork.IncrementalSyncronizationRepository.AddIncrementalSyncronization(new IncrementalSyncronization()
                {
                    StoreId = storeId,
                    DeviceId = deviceId,
                    LastSynced = DateTime.Now,
                    TableName = "Employees"

                });
                _unitOfWork.Complete();
                return Ok(Mapper.Map<EmployeeModelView[]>(data));


            }


        }

        // GET: api/EmployeesSync/5
        public async Task<IHttpActionResult> GetEmployee(int id, int storeId)
        {
            return Ok(await _unitOfWork.EmployeeRepository.GetEmployeeByIdAsync(id, storeId));
        }

        // POST: api/EmployeesSync
        public async Task<IHttpActionResult> AddEmployees([FromBody]SyncObject sync)
        {
            try
            {
                List<Employee> employees = System.Web.Helpers.Json.Decode<List<Employee>>(sync.Object);
                foreach (var employee in employees)
                {
                    employee.Code = employee.Id.ToString();
                    employee.Synced = true;
                    employee.SyncedOn = DateTime.Now;
                    await _unitOfWork.EmployeeRepository.AddEmployeeAsync(employee);
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

        // PUT: api/EmployeesSync/5
        public void UpdateEmployee(int id, [FromBody]string value)
        {
        }

        // DELETE: api/EmployeesSync/5
        public void DeleteEmployee(int id)
        {
        }
    }
}
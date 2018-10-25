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
    public class EmployeesController : ApiController
    {

        private IUnitOfWork _unitOfWork;
        public EmployeesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IHttpActionResult> GetEmployees()
        {
            return Ok(Mapper.Map<EmployeeModelView[]>(_unitOfWork.EmployeeRepository.GetApiEmployees()));
        }

        // GET: api/EmployeesSync/5
        public async Task<IHttpActionResult> GetEmployee(int id, int storeId)
        {
            return Ok(_unitOfWork.EmployeeRepository.GetEmployeeById(id, storeId));
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
                    _unitOfWork.EmployeeRepository.AddEmployee(employee);
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
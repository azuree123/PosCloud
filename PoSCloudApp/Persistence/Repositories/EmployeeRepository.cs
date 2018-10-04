using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using PoSCloud.Persistence;
using PoSCloudApp.Core.Models;
using PoSCloudApp.Core.Repositories;

namespace PoSCloudApp.Persistence.Repositories
{
    public class EmployeeRepository:IEmployeeRepository
    {
        private PosDbContext _context;

        public EmployeeRepository(PosDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Employee> GetEmployees()
        {
            return _context.Employees.ToList();
        }

        public Employee GetEmployeeById(int id)
        {
            return _context.Employees.Find(id);
        }

        public void AddEmployee(Employee employee)
        {
            _context.Employees.Add(employee);

        }

        public void UpdateEmployee(int id, Employee employee)
        {
            if (employee.Id != id)
            {
                employee.Id = id;
            }
            else { }

            _context.Employees.Attach(employee);
            _context.Entry(employee).State = EntityState.Modified;
        }

        public void DeleteEmployee(int id)
        {
            var employee = new Employee {Id = id};
            _context.Employees.Attach(employee);
            _context.Entry(employee).State = EntityState.Deleted;
        }
    }
}
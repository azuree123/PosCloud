using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using POSApp.Core.Models;
using POSApp.Core.Repositories;

namespace POSApp.Persistence.Repositories
{
    public class EmployeeRepository:IEmployeeRepository
    {
        private PosDbContext _context;

        public EmployeeRepository(PosDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Employee> GetEmployees(int storeId)
        {
            return _context.Employees.Where(a=>a.StoreId == storeId).ToList();
        }

        public Employee GetEmployeeById(int id,int storeid)
        {
            return _context.Employees.Find(id, storeid);
        }

        public void AddEmployee(Employee employee)
        {
            _context.Employees.Add(employee);

        }

        public void UpdateEmployee(int id, Employee employee, int storeid)
        {
            if (employee.Id != id)
            {
                employee.Id = id;
            }
            else { }

            employee.StoreId = storeid;
            _context.Employees.Attach(employee);
            _context.Entry(employee).State = EntityState.Modified;
        }

        public void DeleteEmployee(int id, int storeid)
        {
            var employee = new Employee {Id = id, StoreId = storeid};
            _context.Employees.Attach(employee);
            _context.Entry(employee).State = EntityState.Deleted;
        }
        public IEnumerable<Employee> GetApiEmployees()
        {
            IEnumerable<Employee> employees = _context.Employees.Where(a => !a.Synced).ToList();
            foreach (var employee in employees)
            {
                employee.Synced = true;
                employee.SyncedOn = DateTime.Now;
            }

            _context.SaveChanges();
            return employees;
        }
    }
}
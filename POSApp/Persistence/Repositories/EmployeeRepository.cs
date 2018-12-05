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
            return _context.Employees.Where(a=>a.StoreId == storeId && !a.IsDisabled).ToList();
        }

        public Employee GetEmployeeById(int id,int storeid)
        {
            return _context.Employees.Find(id, storeid);
        }

        public void AddEmployee(Employee employee)
        {
            var inDb = _context.Employees.FirstOrDefault(a =>
                a.Name == employee.Name && a.StoreId == employee.StoreId && a.DepartmentId == employee.DepartmentId &&
                a.Email == employee.Email);
            if (inDb == null)
            {
                _context.Employees.Add(employee);
            }
            else
            {
                if (inDb.IsDisabled)
                {
                    employee.Id = inDb.Id;
                    _context.Entry(inDb).CurrentValues.SetValues(employee);
                    _context.Entry(inDb).State = EntityState.Modified;
                }
                else
                {
                    throw new Exception("Entity Already Exists!");
                }
            }

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
            var employee = _context.Employees.FirstOrDefault(a => a.Id == id && a.StoreId == storeid);
            employee.IsDisabled = true;
            _context.Employees.Attach(employee);
            _context.Entry(employee).State = EntityState.Modified;
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
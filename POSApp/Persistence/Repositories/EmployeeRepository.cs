using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsyncIncremental(int storeId, DateTime date)
        {
            return await _context.Employees.Where(a => a.StoreId == storeId && !a.IsDisabled && (a.UpdatedOn >= date || a.CreatedOn >= date)).ToListAsync();
        }
        public IEnumerable<Employee> GetStoreEmployee(int id)
        {
            return _context.Stores.Include(a => a.Employees).Where(a => a.Id == id).Select(a => a.Employees).FirstOrDefault();
        }
        public IEnumerable<Employee> GetEmployees(int storeId)
        {
            var data = _context.Employees.Where(a => a.StoreId == storeId && !a.IsDisabled).ToList();
            return data;
        }
        public async Task<IEnumerable<Employee>> GetEmployeesAsync(int storeId)
        {
            return await _context.Employees.Where(a => a.StoreId == storeId && !a.IsDisabled).ToListAsync();
        }
        public Employee GetEmployeeById(int id,int storeid)
        {
            return _context.Employees.Find(id, storeid);
        }
        public async Task<Employee> GetEmployeeByIdAsync(int id, int storeid)
        {
            return await _context.Employees.FindAsync(id, storeid);
        }
        public void AddEmployee(Employee employee)
        {
            var inDb = _context.Employees.FirstOrDefault(a =>
                a.Email == employee.Email && a.StoreId == employee.StoreId
                );
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
        public async Task AddEmployeeAsync(Employee employee)
        {
            var inDb = await _context.Employees.FirstOrDefaultAsync(a =>
                a.Email == employee.Email && a.StoreId == employee.StoreId
            );
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
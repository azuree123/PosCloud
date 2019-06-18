using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using POSApp.Core.Models;

namespace POSApp.Core.Repositories
{
   public interface IEmployeeRepository
   {
       IEnumerable<Employee> GetEmployees(int storeId);
       Employee GetEmployeeById(int id,int storeid);
       void AddEmployee(Employee employee);
       void UpdateEmployee(int id, Employee employee,int storeid);
       void DeleteEmployee(int id,int storeid);
       IEnumerable<Employee> GetApiEmployees();
       Task<IEnumerable<Employee>> GetEmployeesAsync(int storeId);
       Task<Employee> GetEmployeeByIdAsync(int id, int storeid);
       Task AddEmployeeAsync(Employee employee);
       Task<IEnumerable<Employee>> GetAllEmployeesAsyncIncremental(int storeId, DateTime date);
       IEnumerable<Employee> GetStoreEmployee(int id);
   }
}

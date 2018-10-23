using System.Collections.Generic;
using POSApp.Core.Models;

namespace POSApp.Core.Repositories
{
   public interface IEmployeeRepository
   {
       IEnumerable<Employee> GetEmployees();
       Employee GetEmployeeById(int id,int storeid);
       void AddEmployee(Employee employee);
       void UpdateEmployee(int id, Employee employee,int storeid);
       void DeleteEmployee(int id,int storeid);
       IEnumerable<Employee> GetApiEmployees();
   }
}

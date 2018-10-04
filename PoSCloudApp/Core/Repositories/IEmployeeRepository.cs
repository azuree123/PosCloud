using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoSCloudApp.Core.Models;

namespace PoSCloudApp.Core.Repositories
{
   public interface IEmployeeRepository
   {
       IEnumerable<Employee> GetEmployees();
       Employee GetEmployeeById(int id);
       void AddEmployee(Employee employee);
       void UpdateEmployee(int id, Employee employee);
       void DeleteEmployee(int id);
   }
}

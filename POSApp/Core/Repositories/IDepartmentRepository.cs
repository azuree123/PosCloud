using System.Collections.Generic;
using POSApp.Core.Models;

namespace POSApp.Core.Repositories
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetDepartments();
        void AddDepartment(Department department);
        Department GetDepartmentById(int id);
        void UpdateDepartment(int id, Department department);
        void DeleteDepartment(int id);
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using POSApp.Core.Models;

namespace POSApp.Core.Repositories
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetDepartments(int storeId);
        void AddDepartment(Department department);
        Department GetDepartmentById(int id, int storeId);
        void UpdateDepartment(int id, int storeId, Department department);
        void DeleteDepartment(int id,int storeId);
        IEnumerable<Department> GetApiDepartments();
        Task<IEnumerable<Department>> GetDepartmentsAsync(int storeId);
        Task<Department> GetDepartmentByIdAsync(int id, int storeId);
        Task AddDepartmentAsync(Department department);
    }
}

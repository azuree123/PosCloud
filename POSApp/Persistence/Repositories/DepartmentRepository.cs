using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using POSApp.Core.Models;
using POSApp.Core.Repositories;

namespace POSApp.Persistence.Repositories
{
    public class DepartmentRepository:IDepartmentRepository
    {
        private PosDbContext _context;

        public DepartmentRepository(PosDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Department> GetDepartments(int storeId)
        {
            return _context.Departments.Where(a=>a.StoreId==storeId).ToList();
        }

        public Department GetDepartmentById(int id, int storeId)
        {
            return _context.Departments.Where(a=>a.Id==id && a.StoreId==storeId).ToList().FirstOrDefault();
        }

        public void AddDepartment(Department department)
        {
            _context.Departments.Add(department);
        }

        public void UpdateDepartment(int id, int storeId, Department department)
        {
            department.StoreId = storeId;
            _context.Departments.Attach(department);
            _context.Entry(department).State = EntityState.Modified;
        }

        public void DeleteDepartment(int id, int storeId)
        {
            var department = new Department { Id = id,StoreId = storeId};
            _context.Departments.Attach(department);
            _context.Entry(department).State = EntityState.Deleted;
        }
        public IEnumerable<Department> GetApiDepartments()
        {
            IEnumerable<Department> departments = _context.Departments.Where(a => !a.Synced).ToList();
            foreach (var department in departments)
            {
                department.Synced = true;
                department.SyncedOn = DateTime.Now;
            }

            _context.SaveChanges();
            return departments;
        }
    }
}
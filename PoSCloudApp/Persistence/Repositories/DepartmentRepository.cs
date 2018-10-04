using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using PoSCloudApp.Persistence;
using PoSCloudApp.Core.Models;
using PoSCloudApp.Core.Repositories;

namespace PoSCloudApp.Persistence.Repositories
{
    public class DepartmentRepository:IDepartmentRepository
    {
        private PosDbContext _context;

        public DepartmentRepository(PosDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Department> GetDepartments()
        {
            return _context.Departments.ToList();
        }

        public Department GetDepartmentById(int id)
        {
            return _context.Departments.Find(id);
        }

        public void AddDepartment(Department department)
        {
            _context.Departments.Add(department);
        }

        public void UpdateDepartment(int id, Department department)
        {
            _context.Departments.Attach(department);
            _context.Entry(department).State = EntityState.Modified;
        }

        public void DeleteDepartment(int id)
        {
            var department = new Department { Id = id };
            _context.Departments.Attach(department);
            _context.Entry(department).State = EntityState.Deleted;
        }
    }
}
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
            return _context.Departments.Where(a=>a.StoreId==storeId && !a.IsDisabled).ToList();
        }

        public Department GetDepartmentById(int id, int storeId)
        {
            return _context.Departments.Where(a=>a.Id==id && a.StoreId==storeId).ToList().FirstOrDefault();
        }

        public void AddDepartment(Department department)
        {
            var inDb = _context.Departments.FirstOrDefault(a =>
                a.Name == department.Name && a.StoreId == department.StoreId);
            if (inDb == null)
            {
                _context.Departments.Add(department);
            }
            else
            {
                if (inDb.IsDisabled)
                {
                    department.Id = inDb.Id;
                    _context.Entry(inDb).CurrentValues.SetValues(department);
                    _context.Entry(inDb).State = EntityState.Modified;
                }
                else
                {
                    throw new Exception("Entity Already Exists!");
                }
            }
        }

        public void UpdateDepartment(int id, int storeId, Department department)
        {
            department.StoreId = storeId;
            _context.Departments.Attach(department);
            _context.Entry(department).State = EntityState.Modified;
        }

        public void DeleteDepartment(int id, int storeId)
        {
            var department = _context.Departments.FirstOrDefault(a => a.Id == id && a.StoreId == storeId);
            department.IsDisabled = true;
            _context.Departments.Attach(department);
            _context.Entry(department).State = EntityState.Modified;
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
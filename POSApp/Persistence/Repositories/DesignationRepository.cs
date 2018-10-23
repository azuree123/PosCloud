using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using POSApp.Core.Models;
using POSApp.Core.Repositories;

namespace POSApp.Persistence.Repositories
{
    public class DesignationRepository:IDesignationRepository
    {
        private PosDbContext _context;

        public DesignationRepository(PosDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Designation> GetDesignations()
        {
            return _context.Designations.ToList();
        }

        public Designation GetDesignationById(int id)
        {
            return _context.Designations.Find(id);
        }

        public void AddDesignation(Designation designation)
        {
            _context.Designations.Add(designation);
        }

        public void UpdateDesignation(int id, Designation designation)
        {
            _context.Designations.Attach(designation);
            _context.Entry(designation).State = EntityState.Modified;
        }

        public void DeleteDesignation(int id)
        {
            var designation = new Designation { Id = id };
            _context.Designations.Attach(designation);
            _context.Entry(designation).State = EntityState.Deleted;
        }
        public IEnumerable<Designation> GetApiDesignations()
        {
            IEnumerable<Designation> designations = _context.Designations.Where(a => !a.Synced).ToList();
            foreach (var designation in designations)
            {
                designation.Synced = true;
                designation.SyncedOn = DateTime.Now;
            }

            _context.SaveChanges();
            return designations;
        }
    }
}
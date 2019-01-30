using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using POSApp.Core.Models;
using POSApp.Core.Repositories;

namespace POSApp.Persistence.Repositories
{
    public class DesignationRepository : IDesignationRepository
    {
        private PosDbContext _context;

        public DesignationRepository(PosDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Designation> GetDesignations(int storeId)
        {
            return _context.Designations.Where(a => a.StoreId == storeId && !a.IsDisabled).ToList();
        }
        public async Task<IEnumerable<Designation>> GetDesignationsAsync(int storeId)
        {
            return await _context.Designations.Where(a => a.StoreId == storeId && !a.IsDisabled).ToListAsync();
        }
        public Designation GetDesignationById(int id, int storeId)
        {
            return _context.Designations.Where(a => a.Id == id && a.StoreId == storeId && !a.IsDisabled).ToList().FirstOrDefault();
        }
        public async Task<Designation> GetDesignationByIdAsync(int id, int storeId)
        {
            return await _context.Designations.FirstOrDefaultAsync(a => a.Id == id && a.StoreId == storeId && !a.IsDisabled);
        }
        public void AddDesignation(Designation Designation)
        {
            var inDb = _context.Designations.FirstOrDefault(a =>
                a.Name == Designation.Name && a.StoreId == Designation.StoreId);
            if (inDb == null)
            {
                _context.Designations.Add(Designation);
            }
            else
            {
                if (inDb.IsDisabled)
                {
                    Designation.Id = inDb.Id;
                    _context.Entry(inDb).CurrentValues.SetValues(Designation);
                    _context.Entry(inDb).State = EntityState.Modified;
                }
                else
                {
                    throw new Exception("Entity Already Exists!");
                }
            }
        }
        public async Task AddDesignationAsync(Designation Designation)
        {
            var inDb = await _context.Designations.FirstOrDefaultAsync(a =>
                a.Name == Designation.Name && a.StoreId == Designation.StoreId);
            if (inDb == null)
            {
                _context.Designations.Add(Designation);
            }
            else
            {
                if (inDb.IsDisabled)
                {
                    Designation.Id = inDb.Id;
                    _context.Entry(inDb).CurrentValues.SetValues(Designation);
                    _context.Entry(inDb).State = EntityState.Modified;
                }
                else
                {
                    throw new Exception("Entity Already Exists!");
                }
            }
        }
        public void UpdateDesignation(int id, int storeId, Designation Designation)
        {
            Designation.StoreId = storeId;
            _context.Designations.Attach(Designation);
            _context.Entry(Designation).State = EntityState.Modified;
        }

        public void DeleteDesignation(int id, int storeId)
        {
            var Designation = _context.Designations.FirstOrDefault(a => a.Id == id && a.StoreId == storeId);
            Designation.IsDisabled = true;
            _context.Designations.Attach(Designation);
            _context.Entry(Designation).State = EntityState.Modified;
        }
        public IEnumerable<Designation> GetApiDesignations()
        {
            IEnumerable<Designation> Designations = _context.Designations.Where(a => !a.Synced).ToList();
            foreach (var Designation in Designations)
            {
                Designation.Synced = true;
                Designation.SyncedOn = DateTime.Now;
            }

            _context.SaveChanges();
            return Designations;
        }
    }
}
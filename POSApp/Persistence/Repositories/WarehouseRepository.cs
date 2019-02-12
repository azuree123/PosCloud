using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using POSApp.Core.Models;
using POSApp.Core.Repositories;

namespace POSApp.Persistence.Repositories
{
    public class WarehouseRepository:IWarehouseRepository
    {
        private PosDbContext _context;

        public WarehouseRepository(PosDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Warehouse> GetWarehouses()
        {
            return _context.Warehouses.Include(a => a.Store).Where(a => !a.IsDisabled).ToList();
        }
        public async Task<IEnumerable<Warehouse>> GetWarehousesAsync()
        {
            return await _context.Warehouses.Include(a => a.Store).Where(a => !a.IsDisabled).ToListAsync();
        }
        public IEnumerable<Warehouse> GetWarehouses(int storeId)
        {
            return _context.Warehouses.Where(a => a.StoreId == storeId && !a.IsDisabled).Include(a => a.Store).ToList();
        }

        public Warehouse GetWarehouse(int id)
        {
            return _context.Warehouses.Find(id);
        }
        public async Task<Warehouse> GetWarehouseAsync(int id)
        {
            return await _context.Warehouses.FindAsync(id);
        }
        public void AddWarehouse(Warehouse warehouse)
        {
            var inDb = _context.Warehouses.FirstOrDefault(a => a.Name == warehouse.Name && a.StoreId == warehouse.StoreId);
            if (inDb == null)
            {
                _context.Warehouses.Add(warehouse);
            }
            else
            {
                if (inDb.IsDisabled)
                {
                    warehouse.Id = inDb.Id;
                    _context.Entry(inDb).CurrentValues.SetValues(warehouse);
                    _context.Entry(inDb).State = EntityState.Modified;
                }
                else
                {
                    throw new Exception("Entity Already Exists!");
                }
            }
        }
        public async Task AddWarehouseAsync(Warehouse warehouse)
        {
            var inDb = await _context.Warehouses.FirstOrDefaultAsync(a => a.Name == warehouse.Name && a.StoreId == warehouse.StoreId);
            if (inDb == null)
            {
                _context.Warehouses.Add(warehouse);
            }
            else
            {
                if (inDb.IsDisabled)
                {
                    warehouse.Id = inDb.Id;
                    _context.Entry(inDb).CurrentValues.SetValues(warehouse);
                    _context.Entry(inDb).State = EntityState.Modified;
                }
                else
                {
                    throw new Exception("Entity Already Exists!");
                }
            }
        }

        public void UpdateWarehouse(int id, Warehouse warehouse)
        {
            _context.Warehouses.Attach(warehouse);
            _context.Entry(warehouse).State = EntityState.Modified;
        }

        public void DeleteWarehouse(int id)
        {
            var warehouse = _context.Warehouses.FirstOrDefault(a => a.Id == id);
            warehouse.IsDisabled = true;
            _context.Warehouses.Attach(warehouse);
            _context.Entry(warehouse).State = EntityState.Modified;
        }
        public IEnumerable<Warehouse> GetApiWarehouses()
        {
            IEnumerable<Warehouse> warehouses = _context.Warehouses.Where(a => !a.Synced).ToList();
            foreach (var ware in warehouses)
            {
                ware.Synced = true;
                ware.SyncedOn = DateTime.Now;
            }
            _context.SaveChanges();
            return warehouses;
        }
    }
}

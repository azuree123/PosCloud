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
    public class DineTableRepository:IDineTableRepository
    {
        private PosDbContext _context;

        public DineTableRepository(PosDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<DineTable>> GetAllTablesAsyncIncremental(int storeId, DateTime date)
        {
            return await _context.DineTables.Where(a => a.StoreId == storeId && !a.IsDisabled && (a.UpdatedOn >= date || a.CreatedOn >= date)).ToListAsync();
        }
        public IEnumerable<DineTable> GetDineTables(int storeid)
        {
            return _context.DineTables.Where(a => a.StoreId == storeid && !a.IsDisabled).Include(f => f.Floor).ToList();
        }
        public async Task<IEnumerable<DineTable>> GetDineTablesAsync(int storeid)
        {
            return await _context.DineTables.Where(a => a.StoreId == storeid && !a.IsDisabled).Include(f => f.Floor).ToListAsync();
        }
        public DineTable GetDineTableById(int id, int storeid)
        {
            return _context.DineTables.Find(id, storeid);
        }
        public async Task<DineTable> GetDineTableByIdAsync(int id, int storeid)
        {
            return await _context.DineTables.FindAsync(id, storeid);
        }
        public void AddDineTable(DineTable DineTable)
        {
            var inDb = _context.DineTables.FirstOrDefault(a =>
                a.DineTableNumber == DineTable.DineTableNumber && a.StoreId == DineTable.StoreId &&
                a.FloorId == DineTable.FloorId);
            if (inDb == null)
            {
                _context.DineTables.Add(DineTable);
            }
            else
            {
                if (inDb.IsDisabled)
                {
                    DineTable.Id = inDb.Id;
                    _context.Entry(inDb).CurrentValues.SetValues(DineTable);
                    _context.Entry(inDb).State = EntityState.Modified;
                }
                else
                {
                    throw new Exception("Entity Already Exists!");
                }
            }

        }
        public async Task AddDineTableAsync(DineTable DineTable)
        {
            var inDb = await _context.DineTables.FirstOrDefaultAsync(a =>
                a.DineTableNumber == DineTable.DineTableNumber && a.StoreId == DineTable.StoreId &&
                a.FloorId == DineTable.FloorId);
            if (inDb == null)
            {
                _context.DineTables.Add(DineTable);
            }
            else
            {
                if (inDb.IsDisabled)
                {
                    DineTable.Id = inDb.Id;
                    _context.Entry(inDb).CurrentValues.SetValues(DineTable);
                    _context.Entry(inDb).State = EntityState.Modified;
                }
                else
                {
                    throw new Exception("Entity Already Exists!");
                }
            }

        }
        public void UpdateDineTable(int id, DineTable DineTable, int storeid)
        {
            if (DineTable.Id != id)
            {
                DineTable.Id = id;
            }
            else { }

            DineTable.StoreId = storeid;
            _context.DineTables.Attach(DineTable);
            _context.Entry(DineTable).State = EntityState.Modified;
        }

        public void DeleteDineTable(int id, int storeid)
        {
            var dinetable = _context.DineTables.FirstOrDefault(a => a.Id == id && a.StoreId == storeid);
            dinetable.IsDisabled = true;
            _context.DineTables.Attach(dinetable);
            _context.Entry(dinetable).State = EntityState.Modified;
        }
    }
}
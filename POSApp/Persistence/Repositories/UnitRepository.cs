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
    public class UnitRepository:IUnitRepository
    {
        private PosDbContext _context;

        public UnitRepository(PosDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Unit>> GetAllUnitsAsyncIncremental(int storeId, DateTime date)
        {
            return await _context.Units.Where(a => a.StoreId == storeId && !a.IsDisabled && (a.UpdatedOn >= date || a.CreatedOn >= date)).ToListAsync();
        }
        public IEnumerable<Unit> GetUnit(int storeid)
        {
            return _context.Units.Where(a => a.StoreId == storeid && !a.IsDisabled).ToList();
        }
        public async Task<IEnumerable<Unit>> GetUnitAsync(int storeid)
        {
            return await _context.Units.Where(a => a.StoreId == storeid && !a.IsDisabled).ToListAsync();
        }
        public Unit GetUnitById(int id, int storeid)
        {
            return _context.Units.Find(id,storeid);
        }
        public async Task<Unit> GetUnitByIdAsync(int id, int storeid)
        {
            return await _context.Units.FindAsync(id, storeid);
        }
        public void AddUnit(Unit unit)
        {
            var inDb = _context.Units.FirstOrDefault(a => a.Name == unit.Name && a.StoreId == unit.StoreId);
            if (inDb == null)
            {
                _context.Units.Add(unit);
            }
            else
            {
                if (inDb.IsDisabled)
                {
                    unit.Id = inDb.Id;
                    _context.Entry(inDb).CurrentValues.SetValues(unit);
                    _context.Entry(inDb).State = EntityState.Modified;
                }
                else
                {
                    throw new Exception("Entity Already Exists!");
                }
            }
        }
        public async Task AddUnitAsync(Unit unit)
        {
            var inDb = await _context.Units.FirstOrDefaultAsync(a => a.Name == unit.Name && a.StoreId == unit.StoreId);
            if (inDb == null)
            {
                _context.Units.Add(unit);
            }
            else
            {
                if (inDb.IsDisabled)
                {
                    unit.Id = inDb.Id;
                    _context.Entry(inDb).CurrentValues.SetValues(unit);
                    _context.Entry(inDb).State = EntityState.Modified;
                }
                else
                {
                    throw new Exception("Entity Already Exists!");
                }
            }
        }
        public void UpdateUnit(int id, Unit unit, int storeid)
        {
            if (unit.Id != id)
            {
                unit.Id = id;
            }
            else
            {
            }
            unit.StoreId = storeid;
            _context.Units.Attach(unit);
            _context.Entry(unit).State = EntityState.Modified;
        }

        public void DeleteUnit(int id,int storeId)
        {
            var unit = _context.Units.FirstOrDefault(a => a.Id == id && a.StoreId == storeId);
            unit.IsDisabled = true;
            _context.Units.Attach(unit);
            _context.Entry(unit).State = EntityState.Modified;
        }
        
    }
}
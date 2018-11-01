using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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

        public IEnumerable<Unit> GetUnit(int storeid)
        {
            return _context.Units.Where(a => a.StoreId == storeid).ToList();
        }

        public Unit GetUnitById(int id, int storeid)
        {
            return _context.Units.Find(id,storeid);
        }

        public void AddUnit(Unit unit)
        {
            _context.Units.Add(unit);
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
            var unit = new Unit {Id = id,StoreId = storeId};
            _context.Units.Attach(unit);
            _context.Entry(unit).State = EntityState.Deleted;
        }
        
    }
}
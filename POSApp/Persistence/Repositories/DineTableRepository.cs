using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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

        public IEnumerable<DineTable> GetDineTables(int storeid)
        {
            return _context.DineTables.Where(a => a.StoreId == storeid).ToList();
        }

        public DineTable GetDineTableById(int id, int storeid)
        {
            return _context.DineTables.Find(id, storeid);
        }

        public void AddDineTable(DineTable DineTable)
        {
            if (!_context.DineTables.Where(a => a.DineTableNumber == DineTable.DineTableNumber && a.StoreId == DineTable.StoreId && a.FloorId == DineTable.FloorId).Any())
            {
                _context.DineTables.Add(DineTable);
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
            var DineTable = new DineTable { Id = id, StoreId = storeid };
            _context.DineTables.Attach(DineTable);
            _context.Entry(DineTable).State = EntityState.Deleted;
        }
    }
}
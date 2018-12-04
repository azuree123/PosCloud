using POSApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using POSApp.Core.Repositories;

namespace POSApp.Persistence.Repositories
{
    public class POSTerminalRepository:IPOSTerminalRepository
    {
        
        private PosDbContext _context;

        public POSTerminalRepository(PosDbContext context)
        {
            _context = context;
        }

        public IEnumerable<POSTerminal> GetPOSTerminals(int storeid)
        {
            return _context.PosTerminals.Where(a => a.StoreId == storeid && a.IsActive).Include(f => f.Section).ToList();
        }

        public POSTerminal GetPOSTerminalById(int id, int storeid)
        {
            return _context.PosTerminals.Find(id, storeid);
        }

        public void AddPOSTerminal(POSTerminal POSTerminal)
        {
            if (!_context.PosTerminals.Where(a => a.Name == POSTerminal.Name && a.StoreId == POSTerminal.StoreId && a.SectionId == POSTerminal.SectionId).Any())
            {
                _context.PosTerminals.Add(POSTerminal);
            }

        }

        public void UpdatePOSTerminal(int id, POSTerminal POSTerminal, int storeid)
        {
            if (POSTerminal.POSTerminalId != id)
            {
                POSTerminal.POSTerminalId = id;
            }
            else { }

            POSTerminal.StoreId = storeid;
            _context.PosTerminals.Attach(POSTerminal);
            _context.Entry(POSTerminal).State = EntityState.Modified;
        }

        public void DeletePOSTerminal(int id, int storeid)
        {
            var posTerminal = _context.PosTerminals.FirstOrDefault(a => a.POSTerminalId == id && a.StoreId == storeid);
            posTerminal.IsActive = false;
            _context.PosTerminals.Attach(posTerminal);
            _context.Entry(posTerminal).State = EntityState.Modified;
        }
    }
}

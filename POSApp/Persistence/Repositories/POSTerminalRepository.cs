using POSApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
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
            return _context.PosTerminals.Where(a => a.StoreId == storeid && !a.IsDisabled).Include(f => f.Section).ToList();
        }
        public async Task<IEnumerable<POSTerminal>> GetPOSTerminalsAsync(int storeid)
        {
            return await _context.PosTerminals.Where(a => a.StoreId == storeid && !a.IsDisabled).Include(f => f.Section).ToListAsync();
        }
        public POSTerminal GetPOSTerminalById(int id, int storeid)
        {
            return _context.PosTerminals.Find(id, storeid);
        }
        public async Task<POSTerminal> GetPOSTerminalByIdAsync(int id, int storeid)
        {
            return await _context.PosTerminals.FindAsync(id, storeid);
        }
        public void AddPOSTerminal(POSTerminal POSTerminal)
        {
            var inDb = _context.PosTerminals.FirstOrDefault(a =>
                a.Name == POSTerminal.Name && a.StoreId == POSTerminal.StoreId && a.SectionId == POSTerminal.SectionId);
            if (inDb == null)
            {
                _context.PosTerminals.Add(POSTerminal);
            }
            else
            {
                if (inDb.IsDisabled)
                {
                    POSTerminal.POSTerminalId = inDb.POSTerminalId;
                    _context.Entry(inDb).CurrentValues.SetValues(POSTerminal);
                    _context.Entry(inDb).State = EntityState.Modified;
                }
                else
                {
                    throw new Exception("Entity Already Exists!");
                }
            }

        }
        public async Task AddPOSTerminalAsync(POSTerminal POSTerminal)
        {
            var inDb = await _context.PosTerminals.FirstOrDefaultAsync(a =>
                a.Name == POSTerminal.Name && a.StoreId == POSTerminal.StoreId && a.SectionId == POSTerminal.SectionId);
            if (inDb == null)
            {
                _context.PosTerminals.Add(POSTerminal);
            }
            else
            {
                if (inDb.IsDisabled)
                {
                    POSTerminal.POSTerminalId = inDb.POSTerminalId;
                    _context.Entry(inDb).CurrentValues.SetValues(POSTerminal);
                    _context.Entry(inDb).State = EntityState.Modified;
                }
                else
                {
                    throw new Exception("Entity Already Exists!");
                }
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
            posTerminal.IsDisabled = true;
            _context.PosTerminals.Attach(posTerminal);
            _context.Entry(posTerminal).State = EntityState.Modified;
        }
        public IEnumerable<POSTerminal> GetApiPosTerminals()
        {
            IEnumerable<POSTerminal> posTerminals = _context.PosTerminals.Where(a => !a.Synced).ToList();
            foreach (var posTerminal in posTerminals)
            {
                posTerminal.Synced = true;
                posTerminal.SyncedOn = DateTime.Now;
            }

            _context.SaveChanges();
            return posTerminals;
        }
    }
}

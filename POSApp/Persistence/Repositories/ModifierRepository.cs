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
    public class ModifierRepository:IModifierRepository
    {
        private PosDbContext _context;

        public ModifierRepository(PosDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Modifier> GetModifiers(int storeId)
        {
            return _context.Modifiers.Include(a=>a.ModifierLinkProducts).Include(a=>a.ModifierOptions).Where(x => x.StoreId == storeId && !x.IsDisabled).ToList();
        }
        public async Task<IEnumerable<Modifier>> GetModifiersAsync(int storeId)
        {
            return await _context.Modifiers.Include(a => a.ModifierLinkProducts).Include(a => a.ModifierOptions).Where(x => x.StoreId == storeId && !x.IsDisabled).ToListAsync();
        }
        public Modifier GetModifierById(int id, int storeId)
        {
            return _context.Modifiers.Include(a=>a.ModifierOptions).Where(a => a.Id == id && a.StoreId == storeId).ToList().FirstOrDefault();
        }
        public async Task<Modifier> GetModifierByIdAsync(int id, int storeId)
        {
            return await _context.Modifiers.Include(a => a.ModifierOptions).FirstOrDefaultAsync(a => a.Id == id && a.StoreId == storeId);
        }
        public void AddModifier(Modifier modifier)
        {
            var inDb = _context.Modifiers.FirstOrDefault(a => a.Name == modifier.Name && a.StoreId == modifier.StoreId);
            if (inDb == null)
            {
                _context.Modifiers.Add(modifier);
            }
            else
            {
                if (inDb.IsDisabled)
                {
                    modifier.Id = inDb.Id;
                    _context.Entry(inDb).CurrentValues.SetValues(modifier);
                    _context.Entry(inDb).State = EntityState.Modified;
                }
                else
                {
                    throw new Exception("Entity Already Exists!");
                }
            }
        }
        public async Task AddModifierAsync(Modifier modifier)
        {
            var inDb = await _context.Modifiers.FirstOrDefaultAsync(a => a.Name == modifier.Name && a.StoreId == modifier.StoreId);
            if (inDb == null)
            {
                _context.Modifiers.Add(modifier);
            }
            else
            {
                if (inDb.IsDisabled)
                {
                    modifier.Id = inDb.Id;
                    _context.Entry(inDb).CurrentValues.SetValues(modifier);
                    _context.Entry(inDb).State = EntityState.Modified;
                }
                else
                {
                    throw new Exception("Entity Already Exists!");
                }
            }
        }
        public void UpdateModifier(int id, int storeId, Modifier modifier)
        {
            modifier.StoreId = storeId;
            _context.Modifiers.Attach(modifier);
            _context.Entry(modifier).State = EntityState.Modified;
        }

        public void DeleteModifier(int id, int storeId)
        {
            var modifier = _context.Modifiers.FirstOrDefault(a => a.Id == id && a.StoreId == storeId);
            modifier.IsDisabled = true;
            _context.Modifiers.Attach(modifier);
            _context.Entry(modifier).State = EntityState.Modified;
        }
    }
}
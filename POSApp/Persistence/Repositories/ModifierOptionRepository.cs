using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using POSApp.Core.Models;
using POSApp.Core.Repositories;

namespace POSApp.Persistence.Repositories
{
    public class ModifierOptionRepository:IModifierOptionRepository
    {
        private PosDbContext _context;

        public ModifierOptionRepository(PosDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ModifierOption> GetModifierOptions(int storeId)
        {
            return _context.ModifierOptions.Where(x => x.StoreId == storeId).ToList();
        }

        public ModifierOption GetModifierOptionsById(int id, int storeId)
        {
            return _context.ModifierOptions.Where(a => a.Id == id && a.StoreId == storeId).ToList().FirstOrDefault();
        }

        public void AddModifierOption(ModifierOption mo)
        {
            if (!_context.ModifierOptions.Where(a => a.Name == mo.Name && a.StoreId == mo.StoreId).Any())
            {
                _context.ModifierOptions.Add(mo);
            }
        }

        public void UpdateModifierOptions(int id, int storeId, ModifierOption mo)
        {
            mo.StoreId = storeId;
            _context.ModifierOptions.Attach(mo);
            _context.Entry(mo).State = EntityState.Modified;
        }

        public void DeleteModifierOptions(int id, int storeId)
        {
            ModifierOption mo = new ModifierOption { Id = id, StoreId = storeId };
            _context.ModifierOptions.Attach(mo);
            _context.Entry(mo).State = EntityState.Deleted;
        }
    }
}

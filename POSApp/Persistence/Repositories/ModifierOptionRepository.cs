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
            return _context.ModifierOptions.Where(x => x.StoreId == storeId && !x.IsDisabled).ToList();
        }

        public ModifierOption GetModifierOptionsById(int id, int storeId)
        {
            return _context.ModifierOptions.Where(a => a.Id == id && a.StoreId == storeId).ToList().FirstOrDefault();
        }

        public void AddModifierOption(ModifierOption mo)
        {

            var inDb = _context.ModifierOptions.FirstOrDefault(a => a.Name == mo.Name && a.StoreId == mo.StoreId);
            if (inDb == null)
            {
                _context.ModifierOptions.Add(mo);
            }
            else
            {
                if (inDb.IsDisabled)
                {
                    mo.Id = inDb.Id;
                    _context.Entry(inDb).CurrentValues.SetValues(mo);
                    _context.Entry(inDb).State = EntityState.Modified;
                }
                else
                {
                    throw new Exception("Entity Already Exists!");
                }
            }

            //if (!_context.ModifierOptions.Where(a => a.Name == mo.Name && a.StoreId == mo.StoreId).Any())
            //{
            //    _context.ModifierOptions.Add(mo);
            //}
        }

        public void UpdateModifierOptions(string name, int storeId, ModifierOption mo)
        {
            mo.StoreId = storeId;
            _context.ModifierOptions.Attach(mo);
            _context.Entry(mo).State = EntityState.Modified;
        }

        public void DeleteModifierOptions(int id, int storeId)
        {
            ModifierOption mo = new ModifierOption { Id = id, StoreId = storeId };
            mo.IsDisabled = true;
            _context.ModifierOptions.Attach(mo);
            _context.Entry(mo).State = EntityState.Modified;
        }
        public void DeleteModifierOptionsByModifierId(int id, int storeId)
        {
            List<ModifierOption> modeList = _context.ModifierOptions
                .Where(a => a.ModifierId == id && a.StoreId == storeId).ToList();
            foreach (var modifierOption in modeList)
            {
                _context.ModifierOptions.Remove(modifierOption);
            }
        }
    }
}

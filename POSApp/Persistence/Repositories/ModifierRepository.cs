using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
            return _context.Modifiers.Where(x => x.StoreId == storeId && x.IsActive).ToList();
        }

        public Modifier GetModifierById(int id, int storeId)
        {
            return _context.Modifiers.Include(a=>a.ModifierOptions).Where(a => a.Id == id && a.StoreId == storeId).ToList().FirstOrDefault();
        }

        public void AddModifier(Modifier modifier)
        {
            if (!_context.Modifiers.Where(a=>a.Name == modifier.Name && a.StoreId == modifier.StoreId).Any())
            {
                _context.Modifiers.Add(modifier);
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
            modifier.IsActive = false;
            _context.Modifiers.Attach(modifier);
            _context.Entry(modifier).State = EntityState.Modified;
        }
    }
}
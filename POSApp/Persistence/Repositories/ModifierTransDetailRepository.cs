using POSApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using POSApp.Core.Repositories;

namespace POSApp.Persistence.Repositories
{
    public class ModifierTransDetailRepository : IModifierTransDetailRepository
    {
        private PosDbContext _context;

        public ModifierTransDetailRepository(PosDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ModifierTransDetail> GeModifierTransDetails(int id, int storeId)
        {
            return _context.ModifierTransDetails.Where(a => a.StoreId == storeId && a.TransDetailId == id).ToList();
        }

        public ModifierTransDetail GetModifierTransDetailById(int id, int storeId)
        {
            return _context.ModifierTransDetails.FirstOrDefault(a => a.StoreId == storeId && a.TransDetailId == id);
        }

        public void AddModifierTransDetail(ModifierTransDetail tep)
        {

            if (!_context.ModifierTransDetails.Where( a=>a.StoreId == tep.StoreId && a.TransDetailId == tep.TransDetailId).Any())
            {
                _context.ModifierTransDetails.Add(tep);
            }
        }

        public void UpdateModifierTransDetail(int id, int transDetailId, ModifierTransDetail tep, int storeId)
        {
            if (tep.Id != id)
            {
                tep.Id = id;
            }
            else { }
            if (tep.TransDetailId != transDetailId)
            {
                tep.TransDetailId = transDetailId;
            }
            else { }
            tep.StoreId = storeId;
            _context.ModifierTransDetails.Attach(tep);
            _context.Entry(tep).State = EntityState.Modified;
        }
        public void DeleteModifierTransDetail(int id, int storeId)
        {
            List<ModifierTransDetail> modifiers = _context.ModifierTransDetails
                .Where(a => a.StoreId == storeId && a.TransDetailId == id).ToList();
            foreach (var transModifier in modifiers)
            {

                _context.ModifierTransDetails.Remove(transModifier);
            }

        }
    }
}
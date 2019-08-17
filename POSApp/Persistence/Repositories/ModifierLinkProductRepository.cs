using POSApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using POSApp.Core.Repositories;

namespace POSApp.Persistence.Repositories
{
    public class ModifierLinkProductRepository : IModifierLinkProductRepository
    {
        private PosDbContext _context;

        public ModifierLinkProductRepository(PosDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ModifierLinkProduct> GetModifierLinkProducts(int modifierId,int storeId)
        {
            return _context.ModifierLinkProducts.Where(a=>a.ModifierId==modifierId && a.ModifierStoreId==storeId).ToList();
        }

        public ModifierLinkProduct GetModifierLinkProductById(string ProductCode, int ModifierId)
        {
             return _context.ModifierLinkProducts.FirstOrDefault(a => a.ProductCode == ProductCode && a.ModifierId == ModifierId);
        }
        public IEnumerable<ModifierLinkProduct> GetModifierLinkProductByProductId(string productCode,int storeId)
        {
            return _context.ModifierLinkProducts.Where(a => a.ProductCode == productCode && a.ProductStoreId == storeId).ToList();
        }
        public void AddModifierLinkProducts(ModifierLinkProduct tep)
        {

            if (!_context.ModifierLinkProducts.Where(a => a.ProductCode == tep.ProductCode && a.ModifierId == tep.ModifierId).Any())
            {
                _context.ModifierLinkProducts.Add(tep);
            }
        }

        public void UpdateModifierLinkProducts(string id, int ModifierId, ModifierLinkProduct tep)
        {
            if (tep.ProductCode != id)
            {
                tep.ProductCode = id;
            }
            else { }
            if (tep.ModifierId != ModifierId)
            {
                tep.ModifierId = ModifierId;
            }
            else { }
            
            //  tep.TimedEventStoreId = storeId;

            _context.ModifierLinkProducts.Attach(tep);
            _context.Entry(tep).State = EntityState.Modified;
        }
        public void DeleteModifierLinkProducts(int modifierId, int storeId)
        {
            List<ModifierLinkProduct> products = _context.ModifierLinkProducts
                .Where(a=> a.ModifierId == modifierId && a.ModifierStoreId==storeId).ToList();
            foreach (var timedEventProducts in products)
            {

                _context.ModifierLinkProducts.Remove(timedEventProducts);
            }

        }

        public void DeleteModifierLinkProductsOnly(string productId, int storeId)
        {
            List<ModifierLinkProduct> products = _context.ModifierLinkProducts
                .Where(a => a.ProductCode == productId && a.ProductStoreId == storeId).ToList();
            foreach (var timedEventProducts in products)
            {

                _context.ModifierLinkProducts.Remove(timedEventProducts);
            }

        }
    }
}
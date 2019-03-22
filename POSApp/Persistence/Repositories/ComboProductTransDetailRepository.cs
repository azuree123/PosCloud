using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using POSApp.Core.Models;
using POSApp.Core.Repositories;

namespace POSApp.Persistence.Repositories
{
    public class ComboProductTransDetailRepository:IComboProductTransDetailRepository
    {
        private PosDbContext _context;

        public ComboProductTransDetailRepository(PosDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ComboProductsTransDetail> GetComboProductTransDetail(int id, int storeId)
        {
            return _context.ComboProductsTransDetails.Where(a => a.StoreId == storeId && a.TransDetailId == id).ToList();
        }

        public ComboProductsTransDetail GetComboProductTransDetailById(int id, int storeId)
        {
            return _context.ComboProductsTransDetails.FirstOrDefault(a => a.StoreId == storeId && a.TransDetailId == id);
        }

        public void AddComboProductTransDetail(ComboProductsTransDetail tep)
        {

            if (!_context.ComboProductsTransDetails.Where(a => a.StoreId == tep.StoreId && a.TransDetailId == tep.TransDetailId).Any())
            {
                _context.ComboProductsTransDetails.Add(tep);
            }
        }

        public void UpdateComboProductTransDetail(int id, int transDetailId, ComboProductsTransDetail tep, int storeId)
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
            _context.ComboProductsTransDetails.Attach(tep);
            _context.Entry(tep).State = EntityState.Modified;
        }
        public void DeleteComboProductTransDetail(int id, int storeId)
        {
            List<ComboProductsTransDetail> modifiers = _context.ComboProductsTransDetails
                .Where(a => a.StoreId == storeId && a.TransDetailId == id).ToList();
            foreach (var transModifier in modifiers)
            {

                _context.ComboProductsTransDetails.Remove(transModifier);
            }

        }
    }
}
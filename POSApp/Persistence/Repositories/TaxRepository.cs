using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using POSApp.Core.Models;
using POSApp.Core.Repositories;

namespace POSApp.Persistence.Repositories
{
    public class TaxRepository:ITaxRepository
    {
        private readonly PosDbContext _context;

        public TaxRepository(PosDbContext context)
        {
            _context = context;
        }
        public Tax GetTaxById(int id,int storeId)
        {
            return _context.Taxes.FirstOrDefault(x => x.Id == id && x.StoreId==storeId);
        }
        public IEnumerable<Tax> GetTaxes(int storeId)
        {
            //return _context.Tax;
            return _context.Taxes
                .Where(a => a.StoreId == storeId)
                .ToList();
        }

        public IEnumerable<Tax> GetTaxesFiltered(string query, int storeId)
        {
            //return _context.Tax;
            query = query.ToUpper();
            return _context.Taxes
                    .Where(x => x.Name.ToUpper().Contains(query) && x.StoreId == storeId)
                ;
        }
        public IEnumerable<Tax> GetTaxesFiltered(int query, int storeId)
        {
            return _context.Taxes
                    .Where(x => x.Id == query && x.StoreId == storeId)
                ;
        }

        public int IsExisting(string taxName, int storeId)
        {
            var tax = _context.Taxes.Where(z => z.Name == taxName && z.StoreId == storeId);
            if (tax.Any())
            {
                return tax.FirstOrDefault().Id;
            }
            else
            {
                return 0;
            }

        }
        public void DeleteTax(int id, int storeId)
        {
            var tax = new Tax { Id = id, StoreId = storeId };
            _context.Taxes.Attach(tax);
            _context.Entry(tax).State = EntityState.Deleted;
        }
        public void AddTax(Tax optcategory)
        {
            _context.Taxes.Add(optcategory);

        }
        public void UpdateTax(int id, Tax tax, int storeId)
        {
            if (tax.Id != id)
            {
                tax.Id = id;
            }
            else { }

            tax.StoreId = storeId;
            _context.Taxes.Attach(tax);
            _context.Entry(tax).State = EntityState.Modified;
        }
        public IEnumerable<Tax> GetApiTaxes()
        {
            IEnumerable<Tax> taxes = _context.Taxes.Where(a => !a.Synced).ToList();
            foreach (var tax in taxes)
            {
                tax.Synced = true;
                tax.SyncedOn = DateTime.Now;
            }

            _context.SaveChanges();
            return taxes;
        }
    }
}

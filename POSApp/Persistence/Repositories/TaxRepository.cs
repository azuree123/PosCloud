using System;
using System.Collections.Generic;
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
        public Tax GetTax(int id)
        {
            return _context.Taxes.FirstOrDefault(x => x.Id == id);
        }
        public IEnumerable<Tax> GetTaxes(int storeId)
        {
            //return _context.Tax;
            _context.Taxes.Load();
            return _context.Taxes
                .Local.ToBindingList()
                .Where(a => a.StoreId == storeId)
                .ToList();
        }

        public IEnumerable<Tax> GetTaxesFiltered(string query, int storeId)
        {
            //return _context.Tax;
            query = query.ToUpper();
            return _context.Taxes.Local
                    .Where(x => x.Name.ToUpper().Contains(query) && x.StoreId == storeId)
                ;
        }
        public IEnumerable<Tax> GetTaxesFiltered(int query, int storeId)
        {
            return _context.Taxes.Local
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
        public void Delete(int id, int storeId)
        {
            var dept = _context.Taxes.FirstOrDefault(a => a.Id == id && a.StoreId == storeId);
            _context.Taxes.Remove(dept);
        }
        public void Add(Tax optcategory)
        {
            _context.Taxes.Add(optcategory);

        }
    }
}

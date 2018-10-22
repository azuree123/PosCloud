using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POSApp.Core.Models;

namespace POSApp.Core.Repositories
{
   public interface ITaxRepository
    {
        Tax GetTax(int id);
        IEnumerable<Tax> GetTaxes(int storeId);
        IEnumerable<Tax> GetTaxesFiltered(string query, int storeId);
        IEnumerable<Tax> GetTaxesFiltered(int query, int storeId);
        int IsExisting(string taxName, int storeId);
        void Delete(int id, int storeId);
        void Add(Tax optcategory);
    }
}

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
        Tax GetTaxById(int id,int storeId);
        IEnumerable<Tax> GetTaxes(int storeId);
        IEnumerable<Tax> GetTaxesFiltered(string query, int storeId);
        IEnumerable<Tax> GetTaxesFiltered(int query, int storeId);
        int IsExisting(string taxName, int storeId);
        void DeleteTax(int id, int storeId);
        void AddTax(Tax optcategory);
        Tax GetTaxByCode(string code, int storeId);
        void UpdateTax(int id, Tax tax, int storeId);
        IEnumerable<Tax> GetApiTaxes();
    }
}

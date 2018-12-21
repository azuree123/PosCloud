using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POSApp.Core.Models;
using POSApp.Core.ViewModels;
using POSApp.Core.ViewModels.Sync;

namespace POSApp.Core.Repositories
{
    public interface ITransMasterRepository
    {
        TransMaster GetTransMaster(int id, int storeId);
        IEnumerable<TransMaster> GetTransMasters(int storeId);
        IEnumerable<InvoiceViewModel> GetInvoice(int id, int storeId);
        IEnumerable<TransMasterViewModel> GetTransMastersFiltered(string query, int storeId);
        IEnumerable<TransMasterViewModel> GetTransMastersFiltered(int query, int storeId);
        //int IsExisting(string TransMasterName, int storeId);
        void DeleteTransMaster(int id, int storeId);
        void AddTransMaster(TransMaster transmaster);
        IQueryable<TransMasterViewModel> GetTransMastersQuery(int storeId);
        SalesViewModel GetInvoiceById(int id, int storeId);
    }
}

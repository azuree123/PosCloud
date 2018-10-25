using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POSApp.Core.Models;
using POSApp.Core.ViewModels;

namespace POSApp.Core.Repositories
{
    public interface ITransMasterRepository
    {
        TransMaster GetTransMaster(int id, int storeId);
        IEnumerable<TransMasterViewModel> GetPurchaseOrders(int storeId);
        IEnumerable<InvoiceViewModel> GetInvoice(int id, int storeId);
        IEnumerable<TransMasterViewModel> GetPurchaseOrdersFiltered(string query, int storeId);
        IEnumerable<TransMasterViewModel> GetPurchaseOrdersFiltered(int query, int storeId);
        //int IsExisting(string purchaseOrderName, int storeId);
        void DeleteTransMaster(int id, int storeId);
        void AddTransMaster(TransMaster transmaster);
    }
}

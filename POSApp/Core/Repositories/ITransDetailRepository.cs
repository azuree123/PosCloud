using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POSApp.Core.Models;
using POSApp.Core.ViewModels;

namespace POSApp.Core.Repositories
{
    public interface ITransDetailRepository
    {
        TransDetail GetTransDetail(int id, int storeId);
        IEnumerable<TransDetailViewModel> GetTransDetails(int orderid, int storeId);
        IEnumerable<TransDetailViewModel> GetTransDetailsFiltered(int orderid, string query, int storeId);
        IEnumerable<TransDetailViewModel> GetTransDetailsFiltered(int orderid, int query, int storeId);
        //int IsExisting(string purchaseOrderDetailName, int storeId);
        void DeleteTransDetail(int id, int storeId);
        void AddTransDetail(TransDetail transdetail);
    }
}

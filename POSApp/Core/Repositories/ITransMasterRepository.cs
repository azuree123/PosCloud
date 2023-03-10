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

        Task<IEnumerable<TransMasterListViewModel>> GetLastTransactionINV(string storeId, int deviceId);
        TransMaster GetTransMaster(int id, int storeId);
        IEnumerable<TransMaster> GetTransMasters(int storeId);
        IEnumerable<InvoiceViewModel> GetInvoice(int id, int storeId);
        IEnumerable<TransMasterViewModel> GetTransMastersFiltered(string query, int storeId);
        IEnumerable<TransMasterViewModel> GetTransMastersFiltered(int query, int storeId);
        //int IsExisting(string TransMasterName, int storeId);
        void DeleteTransMaster(int id, int storeId);
        void AddTransMaster(TransMaster transmaster);
        IQueryable<TransMasterViewModel> GetTransMastersQuery(int storeId);
        IEnumerable<TransMaster> GetTransMastersByDate(int storeId);
        IQueryable<TransMasterViewModel> GetDailyTransMastersQuery(int storeId);
        SalesViewModel GetInvoiceById(int id, int storeId);
        Task<TransMaster> GetTransMasterAsync(int id, int storeId);
        Task<IEnumerable<TransMaster>> GetTransMastersAsync(int storeId);
        Task AddTransMasterAsync(TransMaster optcategory);
        IEnumerable<TransMaster> GetSaleInvoices(int storeId);
        decimal AvgPrice(string ingredientCode, int storeId, DateTime date);
        IEnumerable<TransMaster> GetPurchaseInvoices(int storeId);
        TransMaster GetPurchaseById(int id, int storeId);
        void UpdateTransMaster(int id, int storeid, TransMaster transMaster);
        IEnumerable<TransMaster> GetHoldTransactions(int storeId);
        TransMaster GetHoldTransaction(int id, int storeId);
        void DeleteHold(int id, int storeId);

        TransMaster GetSaleTransMaster(int id, int storeId);
        IQueryable<TransMasterViewModel> GetDailyRefundsQuery(int storeId);

        decimal GetSaleInvoicesTotalBySessionCode(string userId, int sessionCode, int storeId, DateTime dateFrom,
            DateTime dateTo);

        IEnumerable<TopEmployeeSaleViewModel> GetTopEmployeeSale();
        decimal GetWeeklyIncome();
        IEnumerable<WeeklyTimeSalesViewModel> GetTimeSale();
        void UpdatePurchasing(int id, int storeid, TransMaster transMaster);
        void UpdateExpiry(int id, int storeid, TransMaster transMaster);
        decimal GetBeforeWeeklyIncome();
        IEnumerable<TransMaster> GetTransMasterProducts(int id, int storeId);
        decimal ProductStock(string ingredientCode, int storeId);
    }
}

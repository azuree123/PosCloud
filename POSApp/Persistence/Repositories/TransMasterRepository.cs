using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using POSApp.Core.Models;
using POSApp.Core.Repositories;
using POSApp.Core.ViewModels;
using POSApp.Core.ViewModels.Sync;
using TransDetailViewModel = POSApp.Core.ViewModels.Sync.TransDetailViewModel;

namespace POSApp.Persistence.Repositories
{
    class TransMasterRepository : ITransMasterRepository
    {
        private readonly PosDbContext _context;

        public TransMasterRepository(PosDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TransMasterListViewModel>> GetLastTransactionINV(string storeId,int deviceId)
        {
            var parameters = new List<SqlParameter> { new SqlParameter("@d", storeId), new SqlParameter("@e", deviceId) };
            
            var sql = @"exec GetLastInvNo @d , @e";

            return  _context.Database.SqlQuery<TransMasterListViewModel>(sql, parameters.ToArray()).ToList();
        }

        public TransMaster GetTransMaster(int id, int storeId)
        {
         var data= _context.TransMasters.Include(a => a.BusinessPartner).Include(a=>a.DineTable).Include(a=>a.TimedEvent)
                .Include(a=>a.TransDetails).Include(a=>a.Device).Include(a=>a.TransDetails.Select(c=>c.Product)).Include(a => a.TransDetails.Select(c => c.ModifierTransDetail))
             .Include(a => a.TransDetails.Select(c => c.ModifierTransDetail.Select(f=>f.ModifierOption)))
            .Include(a=>a.TransMasterPaymentMethods)
                .FirstOrDefault(x => x.Id == id && x.StoreId == storeId);
           

            return data;
        }
        public TransMaster GetSaleTransMaster(int id, int storeId)
        {
            var data = _context.TransMasters.Include(a => a.BusinessPartner).Include(a=>a.DineTable)
                .Include(a => a.TransDetails).Include(a => a.TransDetails.Select(c => c.Product))
                .Include(a => a.TransMasterPaymentMethods).Include(a => a.Store).Include(a => a.Store.Client)
                .FirstOrDefault(x => x.Id == id && x.StoreId == storeId);


            return data;
        }
        public IEnumerable<TransMaster> GetTransMasterProducts(int id, int storeId)
        {
            var parameters = new List<SqlParameter> { new SqlParameter("@p1", storeId), new SqlParameter("@p2", id) };
            var sql = @" exec GetTransMasterProducts @p2,@p1 
                ";
            var data =  _context.Database.SqlQuery<TransMaster>(sql, parameters.ToArray()).ToList();
            return data;



        }

        public decimal ProductStock(string ingredientCode, int storeId)
        {
            var parameters = new List<SqlParameter> { new SqlParameter("@p1",storeId), new SqlParameter("@p3", ingredientCode) };
            
            var sql = @"exec stock @p3,@p1

			            ";
            var stk = Convert.ToDecimal(_context.Database.SqlQuery<decimal>(sql, parameters.ToArray()).ToList().Sum());
            return stk;
        }
        public IQueryable<TransMasterViewModel> GetDailyRefundsQuery(int storeId)
        {
            DateTime today = DateTime.Now;
            //return _context.PurchaseOrder;
            return Mapper.Map<TransMasterViewModel[]>(_context.TransMasters
                .Where(a => a.StoreId == storeId && a.TransDate >= DateTime.Today).Include(a => a.BusinessPartner).Include(a => a.TransMasterPaymentMethods)).AsQueryable();

        }
        public TransMaster GetPurchaseById(int id, int storeId)
        {
            var data = _context.TransMasters.Include(a => a.BusinessPartner).Include(a => a.DineTable).Include(a => a.TimedEvent)
                .Include(a => a.TransDetails).Include(a => a.TransDetails.Select(c => c.Product)).Include(a => a.TransDetails.Select(c => c.ModifierTransDetail))
                .Include(a => a.TransDetails.Select(c => c.ModifierTransDetail.Select(f => f.ModifierOption)))
                .Include(a => a.TransMasterPaymentMethods)
                .FirstOrDefault(x => x.Id == id && x.StoreId == storeId);


            return data;
        }
        public async Task<TransMaster> GetTransMasterAsync(int id, int storeId)
        {
            var data = await _context.TransMasters.Include(a => a.BusinessPartner).Include(a => a.DineTable).Include(a => a.TimedEvent)
                .Include(a => a.TransDetails).Include(a => a.TransDetails.Select(c => c.Product)).Include(a => a.TransDetails.Select(c => c.ModifierTransDetail))
                .Include(a => a.TransDetails.Select(c => c.ModifierTransDetail.Select(f => f.ModifierOption)))
                .Include(a => a.TransMasterPaymentMethods)
                .FirstOrDefaultAsync(x => x.Id == id && x.StoreId == storeId);


            return data;
        }
        public IEnumerable<TransMaster> GetTransMasters(int storeId)
        {
            //return _context.PurchaseOrder;
            var tt= _context.TransMasters.Include(a=>a.Store).Include(a=>a.BusinessPartner).Include(a => a.Warehouse)
                .Where(a => a.StoreId == storeId  && !a.IsDisabled);

            return tt;

        }
        public IEnumerable<TransMaster> GetWareTransMasters(int storeId,int warehouseId)
        {
            //return _context.PurchaseOrder;
            return _context.TransMasters.Include(a => a.Store).Include(a => a.BusinessPartner)
                .Where(a => a.StoreId == storeId &&a.WarehouseId == warehouseId && !a.IsDisabled);

        }
        public IEnumerable<TransMaster> GetHoldTransactions(int storeId)
        {
            //return _context.PurchaseOrder;
            return _context.TransMasters.Include(a=>a.BusinessPartner)
                .Where(a => a.StoreId == storeId && !a.IsDisabled && a.TransStatus=="Hold");

        }
        public TransMaster GetHoldTransaction(int id,int storeId)
        {
            //return _context.PurchaseOrder;
            return _context.TransMasters.Include(a=>a.TransDetails).Include(a=>a.TransDetails.Select(g=>g.Product))
                .FirstOrDefault(a => a.StoreId == storeId && !a.IsDisabled && a.TransStatus == "Hold" && a.Id==id);

        }
        public IEnumerable<TransMaster> GetSaleInvoices(int storeId)
        {
            
            return _context.TransMasters.Include(a=>a.TransDetails)
                .Where(a => a.StoreId == storeId && a.Type == "INV" && !a.Issued && !a.IsDisabled);

        }

        public decimal GetSaleInvoicesTotalBySessionCode(string userId,int sessionCode,int storeId,DateTime dateFrom,DateTime dateTo)
        {

            return _context.TransMasters
                .Where(a => a.StoreId == storeId && a.TransDate>=dateFrom && a.TransDate<=dateTo && a.Type == "INV" && a.SessionCode==sessionCode && !a.Issued && !a.IsDisabled && (a.CreatedById==userId||a.UpdatedById==userId)).Any()?
                _context.TransMasters
                    .Where(a => a.StoreId == storeId && a.TransDate >= dateFrom && a.TransDate <= dateTo && a.Type == "INV" && a.SessionCode == sessionCode && !a.Issued && !a.IsDisabled && (a.CreatedById == userId || a.UpdatedById == userId)).Sum(a => a.TotalPrice):0;

        }
        public IEnumerable<TransMaster> GetPurchaseInvoices(int storeId)
        {

            return _context.TransMasters.Include(a => a.TransDetails)
                .Where(a => a.StoreId == storeId && a.Type == "PRO" && !a.Issued && !a.IsDisabled);

        }
        public async Task<IEnumerable<TransMaster>> GetTransMastersAsync(int storeId)
        {
            //return _context.PurchaseOrder;
            return await _context.TransMasters.Include(a => a.BusinessPartner)
                .Where(a => a.StoreId == storeId && !a.Issued && !a.IsDisabled).ToListAsync();

        }
        public IEnumerable<WeeklyTimeSalesViewModel> GetTimeSale()
        {
            var parameters = new List<SqlParameter> { new SqlParameter("@p1", DateTime.Today.AddDays(-7)) };
            var sql = @"exec GetTimeSale @p1";

            return _context.Database.SqlQuery<WeeklyTimeSalesViewModel>(sql, parameters.ToArray()).ToList();

        }
        public IEnumerable<TopEmployeeSaleViewModel> GetTopEmployeeSale()
        {
            var parameters = new List<SqlParameter> { new SqlParameter("@p1", DateTime.Today.AddDays(-7)) };
            var sql = @"exec GetTopEmployeeSale @p1";

            return _context.Database.SqlQuery<TopEmployeeSaleViewModel>(sql, parameters.ToArray()).ToList();

        }
        public decimal GetWeeklyIncome()
        {
            var parameters = new List<SqlParameter> { new SqlParameter("@p1", DateTime.Today.AddDays(-7)) };
            var sql =
                "exec WeeklyIncome @p1";
            return _context.Database.SqlQuery<decimal>(sql, parameters.ToArray()).FirstOrDefault();
        }
        public decimal GetBeforeWeeklyIncome()
        {
            var parameters = new List<SqlParameter> { new SqlParameter("@p1", DateTime.Today.AddDays(-14)) };
            var sql =
                "exec WeeklyBeforeIncome @p1";
            return _context.Database.SqlQuery<decimal>(sql, parameters.ToArray()).Sum();
        }
        public IEnumerable<TransMaster> GetTransMastersByDate(int storeId)
        {
            DateTime next = DateTime.Today.AddDays(1);
            //return _context.PurchaseOrder;
            return _context.TransMasters.Include(a => a.BusinessPartner)
                .Where(a => a.StoreId == storeId && a.TransDate >= DateTime.Today && a.TransDate< next && !a.Issued && !a.IsDisabled);

        }
        public IEnumerable<InvoiceViewModel> GetInvoice(int id, int storeId)
        {
            var parameters = new List<SqlParameter> { new SqlParameter("@p1", id), new SqlParameter("@p2", storeId) };
            var sql = @"exec GetInvoice @p1 , @p2";

            return _context.Database.SqlQuery<InvoiceViewModel>(sql, parameters.ToArray()).ToList();
            
        }
        public IEnumerable<TransMasterViewModel> GetTransMastersFiltered(string query, int storeId)
        {
            //return _context.PurchaseOrder;
            query = query.ToUpper();
            return _context.TransMasters
                .Where(x => x.StoreId==storeId)
                .Select(p => new TransMasterViewModel { Id = p.Id });
        }
        public IEnumerable<TransMasterViewModel> GetTransMastersFiltered(int query, int storeId)
        {
            return _context.TransMasters
                .Where(x => x.Id == query && x.StoreId == storeId)
                .Select(p => new TransMasterViewModel { Id = p.Id });
        }

       
        public void DeleteTransMaster(int id, int storeId)
        {
            var transDetails = _context.TransDetails.Where(a => a.TransMasterId == id && a.StoreId == storeId)
                .ToList();
            _context.TransDetails.RemoveRange(transDetails);
            var transMaster = _context.TransMasters.FirstOrDefault(a => a.Id == id && a.StoreId == storeId);
          
            _context.TransMasters.Remove(transMaster);
            _context.Entry(transMaster).State = EntityState.Deleted;
        }
        public void AddTransMaster(TransMaster optcategory)
        {
             _context.TransMasters.Add(optcategory);
            
        }

        public void DeleteHold (int id,int storeId)
        {
            var delete =
                _context.TransMasters.Include(a=>a.TransMasterPaymentMethods).Include(a=>a.TransDetails).FirstOrDefault(a => a.Id == id && a.StoreId == storeId && a.TransStatus == "Hold");
            if (delete != null)
            {
                _context.TransDetails.RemoveRange(delete.TransDetails);
                _context.TransMasterPaymentMethods.RemoveRange(delete.TransMasterPaymentMethods);
                _context.TransMasters.Remove(delete);
            }

        }

        public decimal AvgPrice(string ingredientCode, int storeId,DateTime date)
        {
         
            var parameters = new List<SqlParameter> { new SqlParameter("@p1", storeId), new SqlParameter("@p2", date), new SqlParameter("@p3", ingredientCode) };
            var sql = @"exec AvgPrice @p3,@p1,@p2
                ";
            var data = Convert.ToDecimal(_context.Database.SqlQuery<decimal?>(sql, parameters.ToArray()).ToList().Sum());
            return data;
        }
        public async Task AddTransMasterAsync(TransMaster optcategory)
        {
            _context.TransMasters.Add(optcategory);

        }
        public IQueryable<TransMasterViewModel> GetTransMastersQuery(int storeId)
        {
            //return _context.PurchaseOrder;
            return Mapper.Map<TransMasterViewModel[]>(_context.TransMasters
                .Where(a => a.StoreId == storeId).Include(a=>a.BusinessPartner).Include(a=>a.Device).Include(a=>a.TransMasterPaymentMethods)).AsQueryable();

        }
        public IQueryable<TransMasterViewModel> GetDailyTransMastersQuery(int storeId)
        {
            DateTime today = DateTime.Now;
            //return _context.PurchaseOrder;
            return Mapper.Map<TransMasterViewModel[]>(_context.TransMasters
                .Where(a => a.StoreId == storeId && a.TransDate >= DateTime.Today).Include(a => a.BusinessPartner).Include(a => a.TransMasterPaymentMethods)).AsQueryable();

        }
        public SalesViewModel GetInvoiceById(int id, int storeId)
        {
            SalesViewModel salesViewModel=new SalesViewModel();
            salesViewModel.TransMaster = _context.TransMasters.Include(a=>a.TransDetails).Include(a=>a.TransDetails.Select(g=>g.ModifierTransDetail)).FirstOrDefault(a => a.Id == id && a.StoreId == storeId);
            //salesViewModel.TransMaster.TransDetails = _context.TransDetails.Include(a => a.ModifierTransDetail)
            //    .Where(a => a.TransMasterId == id).ToList();
            salesViewModel.TransMaster.TransMasterPaymentMethods =
                _context.TransMasterPaymentMethods.Where(a => a.TransMasterId == id).ToList();
            return salesViewModel;
        }
        public void UpdateTransMaster(int id, int storeid, TransMaster transMaster)
        {
            transMaster.StoreId = storeid;
            _context.TransMasters.Attach(transMaster);
            _context.Entry(transMaster).State = EntityState.Modified;
        }
        public void UpdatePurchasing(int id, int storeid, TransMaster transMaster)
        {
            var transDetails = _context.TransDetails.Where(a => a.TransMasterId == id && a.StoreId == storeid)
                .ToList();
            _context.TransDetails.RemoveRange(transDetails);
            transMaster.StoreId = storeid;
            _context.TransMasters.Attach(transMaster);
            foreach (var transMasterTransDetail in transMaster.TransDetails)
            {
                _context.TransDetails.Add(transMasterTransDetail);
            }
            _context.Entry(transMaster).State = EntityState.Modified;
        }
        public void UpdateExpiry(int id, int storeid, TransMaster transMaster)
        {
            var transDetails = _context.TransDetails.Where(a => a.TransMasterId == id && a.StoreId == storeid)
                .ToList();
            _context.TransDetails.RemoveRange(transDetails);
            transMaster.StoreId = storeid;
            _context.TransMasters.Attach(transMaster);
            foreach (var transMasterTransDetail in transMaster.TransDetails)
            {
                _context.TransDetails.Add(transMasterTransDetail);
            }

            _context.Entry(transMaster).State = EntityState.Modified;
        }
    }
}
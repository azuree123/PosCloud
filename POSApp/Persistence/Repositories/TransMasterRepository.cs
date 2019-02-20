using System;
using System.Collections.Generic;
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
        public TransMaster GetTransMaster(int id, int storeId)
        {
         var data= _context.TransMasters.Include(a => a.BusinessPartner).Include(a=>a.DineTable).Include(a=>a.TimedEvent)
                .Include(a=>a.TransDetails).Include(a=>a.TransDetails.Select(c=>c.Product)).Include(a => a.TransDetails.Select(c => c.ModifierTransDetail))
             .Include(a => a.TransDetails.Select(c => c.ModifierTransDetail.Select(f=>f.ModifierOption)))
            .Include(a=>a.TransMasterPaymentMethods)
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
            return _context.TransMasters.Include(a=>a.BusinessPartner)
                .Where(a => a.StoreId == storeId && !a.Issued && !a.IsDisabled);

        }
        public IEnumerable<TransMaster> GetSaleInvoices(int storeId)
        {
            
            return _context.TransMasters.Include(a => a.BusinessPartner)
                .Where(a => a.StoreId == storeId && a.Type == "Paid" && !a.Issued && !a.IsDisabled);

        }
        public async Task<IEnumerable<TransMaster>> GetTransMastersAsync(int storeId)
        {
            //return _context.PurchaseOrder;
            return await _context.TransMasters.Include(a => a.BusinessPartner)
                .Where(a => a.StoreId == storeId && !a.Issued && !a.IsDisabled).ToListAsync();

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
            var sql = @"SELECT  TransMaster.TransCode, TransMaster.TransDate, TransMaster.TransStatus, TransMaster.PaymentMethod, TransMaster.CreatedOn,
                TransMaster.CreatedById, Products.ProductCode, Products.Name AS Product, Products.Attribute,Products.Size, BusinessPartners.Name AS Customer,
                Units.Code AS Unit, ApplicationUsers.UserName,TransDetails.Quantity, TransDetails.UnitPrice
                FROM TransMaster INNER JOIN
                TransDetails ON TransMaster.Id = TransDetails.TransMasterId AND TransMaster.StoreId = TransDetails.StoreId INNER JOIN
                Products ON TransDetails.ProductId = Products.Id AND TransDetails.StoreId = Products.StoreId INNER JOIN
                BusinessPartners ON TransMaster.BusinessPartnerId = BusinessPartners.Id AND TransMaster.StoreId = BusinessPartners.StoreId INNER JOIN
                Units ON Products.CategoryId = Units.Id AND Products.StoreId = Units.StoreId INNER JOIN
                ApplicationUsers ON TransMaster.StoreId = ApplicationUsers.StoreId AND TransMaster.CreatedById = ApplicationUsers.Id
                WHERE (TransMaster.Id = @p1) AND (TransMaster.StoreId = @p2)";

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
            var transMaster = _context.TransMasters.FirstOrDefault(a => a.Id == id && a.StoreId == storeId);
            transMaster.IsDisabled = true;
            _context.TransMasters.Attach(transMaster);
            _context.Entry(transMaster).State = EntityState.Modified;
        }
        public void AddTransMaster(TransMaster optcategory)
        {
            _context.TransMasters.Add(optcategory);

        }

        public List<FifoHelper> GetCostPriceWithFifo(string ingredientCode, int storeId,decimal qty)
        {
           List<FifoHelper>helper=new List<FifoHelper>();
            var parameters = new List<SqlParameter> { new SqlParameter("@p1", storeId) };
            var sql = @"select b.*
			from PosCloud.TransMaster as a 
            inner join PosCloud.TransDetails as b on a.id=b.TransMasterId AND a.StoreId=b.StoreId
           where a.StoreId=@p1 and a.Type='PRI' and b.Quantity-b.Balance!=0
		   order by a.Id
                ";
            var data = _context.Database.SqlQuery<TransDetail>(sql, parameters.ToArray()).ToList();
            while (qty>1)
            {
             
                var detail = data.Where(a=>a.Quantity-a.Balance!=0).FirstOrDefault();
                if (qty > detail.Quantity)
                {
                    detail.Balance = detail.Quantity;
                    qty -= detail.Quantity;
                }
                else
                {
                    detail.Balance = detail.Quantity - qty;
                    qty = 0;
                }

            }
            return helper;
        }
        public async Task AddTransMasterAsync(TransMaster optcategory)
        {
            _context.TransMasters.Add(optcategory);

        }
        public IQueryable<TransMasterViewModel> GetTransMastersQuery(int storeId)
        {
            //return _context.PurchaseOrder;
            return Mapper.Map<TransMasterViewModel[]>(_context.TransMasters
                .Where(a => a.StoreId == storeId).Include(a=>a.BusinessPartner).Include(a=>a.TransMasterPaymentMethods)).AsQueryable();

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
            salesViewModel.TransMaster = _context.TransMasters.FirstOrDefault(a => a.Id == id && a.StoreId == storeId);
            salesViewModel.TransMaster.TransDetails = _context.TransDetails.Include(a => a.ModifierTransDetail)
                .Where(a => a.TransMasterId == id).ToList();
            salesViewModel.TransMaster.TransMasterPaymentMethods =
                _context.TransMasterPaymentMethods.Where(a => a.TransMasterId == id).ToList();
            return salesViewModel;
        }

    }
}
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using POSApp.Core.Models;
using POSApp.Core.Repositories;
using POSApp.Core.ViewModels;

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
            return _context.TransMasters
                .FirstOrDefault(x => x.Id == id && x.StoreId == storeId);

        }
        public IEnumerable<TransMasterViewModel> GetPurchaseOrders(int storeId)
        {
            //return _context.PurchaseOrder;
            return _context.TransMasters
                .Where(a => a.StoreId == storeId)
                .Select(p => new TransMasterViewModel { Id = p.Id }).ToList();
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
            //  .Local.ToBindingList();
            //_context.TransMasters.Load();
            //return _context.TransMasters
            //     .Local.ToBindingList()   
            //    .Include(d => d.TransDetails.Select(p => p.Product).Select(u => u.ProductUnit)              
            //    .Where(a => a.StoreId == storeId)
            // .Select(p => new TransMasterViewModel { Id = p.Id }).ToList();
        }
        public IEnumerable<TransMasterViewModel> GetPurchaseOrdersFiltered(string query, int storeId)
        {
            //return _context.PurchaseOrder;
            query = query.ToUpper();
            return _context.TransMasters
                //.Where(x => x.Name.ToUpper().Contains(query))
                .Select(p => new TransMasterViewModel { Id = p.Id });
        }
        public IEnumerable<TransMasterViewModel> GetPurchaseOrdersFiltered(int query, int storeId)
        {
            return _context.TransMasters
                .Where(x => x.Id == query && x.StoreId == storeId)
                .Select(p => new TransMasterViewModel { Id = p.Id });
        }

        //public int IsExisting(string purchaseOrderName, int storeId)
        //{
        //    var purchaseOrder = _context.PurchaseOrders.Where(z => z.Name == purchaseOrderName && z.StoreId == storeId);
        //    if (purchaseOrder.Any())
        //    {
        //        return purchaseOrder.FirstOrDefault().Id;
        //    }
        //    else
        //    {
        //        return 0;
        //    }

        //}
        public void DeleteTransMaster(int id, int storeId)
        {
            var dept = _context.TransMasters.FirstOrDefault(a => a.Id == id && a.StoreId == storeId);
            _context.TransMasters.Remove(dept);
        }
        public void AddTransMaster(TransMaster optcategory)
        {
            _context.TransMasters.Add(optcategory);

        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using AutoMapper;
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
        public IEnumerable<TransMaster> GetTransMasters(int storeId)
        {
            //return _context.PurchaseOrder;
            return _context.TransMasters.Include(a=>a.BusinessPartner)
                .Where(a => a.StoreId == storeId && a.IsActive)
                ;
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
            transMaster.IsActive = false;
            _context.TransMasters.Attach(transMaster);
            _context.Entry(transMaster).State = EntityState.Modified;
        }
        public void AddTransMaster(TransMaster optcategory)
        {
            _context.TransMasters.Add(optcategory);

        }
        public IQueryable<TransMasterViewModel> GetTransMastersQuery(int storeId)
        {
            //return _context.PurchaseOrder;
            return Mapper.Map<TransMasterViewModel[]>(_context.TransMasters
                .Where(a => a.StoreId == storeId).Include(a=>a.BusinessPartner)).AsQueryable();

        }

    }
}
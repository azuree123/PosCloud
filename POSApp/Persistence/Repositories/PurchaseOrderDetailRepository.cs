using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using POSApp.Core.Models;
using POSApp.Core.Repositories;

namespace POSApp.Persistence.Repositories
{
    public class PurchaseOrderDetailRepository:IPurchaseOrderDetailRepository
    {
        private PosDbContext _context;

        public PurchaseOrderDetailRepository(PosDbContext context)
        {
            _context = context;
        }

        public IEnumerable<PurchaseOrderDetail> GetPurchaseOrderDetails(int purchaseOrderId)
        {
            return _context.PurchaseOrderDetails.Where(a=>a.PurchaseOrderId == purchaseOrderId).ToList();
        }

        public PurchaseOrderDetail GetPurchaseOrderDetailById(int id)
        {
            return _context.PurchaseOrderDetails.Find(id);
        }

        public void AddPurchaseOrderDetail(PurchaseOrderDetail purchaseOrderDetail)
        {
            _context.PurchaseOrderDetails.Add(purchaseOrderDetail);
        }

        public void UpdatePurchaseOrderDetail(int id, PurchaseOrderDetail purchaseOrderDetail)
        {
            _context.PurchaseOrderDetails.Attach(purchaseOrderDetail);
            _context.Entry(purchaseOrderDetail).State = EntityState.Modified;
        }

        public void DeletePurchaseOrderDetail(int id)
        {
            var purchaseOrderDetail = new PurchaseOrderDetail { Id = id };
            _context.PurchaseOrderDetails.Attach(purchaseOrderDetail);
            _context.Entry(purchaseOrderDetail).State = EntityState.Deleted;
        }
    }
}
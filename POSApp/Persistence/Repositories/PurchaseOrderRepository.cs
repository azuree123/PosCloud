using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using POSApp.Core.Models;
using POSApp.Core.Repositories;

namespace POSApp.Persistence.Repositories
{
    public class PurchaseOrderRepository:IPurchaseOrderRepository
    {
        private PosDbContext _context;

        public PurchaseOrderRepository(PosDbContext context)
        {
            _context = context;
        }

        public IEnumerable<PurchaseOrder> GetPurchaseOrders(int storeId)
        {
            return _context.PurchaseOrders.Where(a=>a.StoreId==storeId).ToList();
        }

        public PurchaseOrder GetPurchaseOrderById(int id)
        {
            return _context.PurchaseOrders.Find(id);
        }

        public void AddPurchaseOrder(PurchaseOrder purchaseOrder)
        {
            _context.PurchaseOrders.Add(purchaseOrder);
        }

        public void UpdatePurchaseOrder(int id, PurchaseOrder purchaseOrder)
        {
            _context.PurchaseOrders.Attach(purchaseOrder);
            _context.Entry(purchaseOrder).State = EntityState.Modified;
        }

        public void DeletePurchaseOrder(int id)
        {
            var purchaseOrder = new PurchaseOrder { Id = id };
            _context.PurchaseOrders.Attach(purchaseOrder);
            _context.Entry(purchaseOrder).State = EntityState.Deleted;
        }
        public IEnumerable<PurchaseOrder> GetApiPurchaseOrders()
        {
            IEnumerable<PurchaseOrder> purchaseOrders = _context.PurchaseOrders.Where(a => !a.Synced).ToList();
            foreach (var purchaseOrder in purchaseOrders)
            {
                purchaseOrder.Synced = true;
                purchaseOrder.SyncedOn = DateTime.Now;
            }

            _context.SaveChanges();
            return purchaseOrders;
        }
    }
}
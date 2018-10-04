using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using PoSCloudApp.Persistence;
using PoSCloudApp.Core.Models;
using PoSCloudApp.Core.Repositories;

namespace PoSCloudApp.Persistence.Repositories
{
    public class PurchaseOrderRepository:IPurchaseOrderRepository
    {
        private PosDbContext _context;

        public PurchaseOrderRepository(PosDbContext context)
        {
            _context = context;
        }

        public IEnumerable<PurchaseOrder> GetPurchaseOrders()
        {
            return _context.PurchaseOrders.ToList();
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
    }
}
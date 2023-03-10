using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using POSApp.Core.Models;
using POSApp.Core.Repositories;

namespace POSApp.Persistence.Repositories
{
    public class SaleOrderRepository:ISaleOrderRepository
    {
        private PosDbContext _context;

        public SaleOrderRepository(PosDbContext context)
        {
            _context = context;
        }

        public IEnumerable<SaleOrder> GetSaleOrders()
        {
            return _context.SaleOrders.Include(a=>a.Customer).ToList();
        }

        public SaleOrder GetSaleOrderById(int id)
        {
            return _context.SaleOrders.Find(id);
        }

        public void AddSaleOrder(SaleOrder saleOrder)
        {
            _context.SaleOrders.Add(saleOrder);
        }

        public void UpdateSaleOrder(int id, SaleOrder saleOrder)
        {
            _context.SaleOrders.Attach(saleOrder);
            _context.Entry(saleOrder).State = EntityState.Modified;
        }

        public void DeleteSaleOrder(int id)
        {
            var saleOrder = new SaleOrder { Id = id };
            _context.SaleOrders.Attach(saleOrder);
            _context.Entry(saleOrder).State = EntityState.Deleted;
        }
    }
}
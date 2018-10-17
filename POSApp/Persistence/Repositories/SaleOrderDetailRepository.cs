using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using POSApp.Core.Models;
using POSApp.Core.Repositories;

namespace POSApp.Persistence.Repositories
{
    public class SaleOrderDetailRepository:ISaleOrderDetailRepository
    {
        private PosDbContext _context;

        public SaleOrderDetailRepository(PosDbContext context)
        {
            _context = context;
        }

        public IEnumerable<SaleOrderDetail> GetSaleOrderDetails(int saleOrderId)
        {
            return _context.SaleOrderDetails.Where(a => a.SaleOrderId == saleOrderId).Include(a=>a.Product).ToList();
        }

        public SaleOrderDetail GetSaleOrderDetailById(int id)
        {
            return _context.SaleOrderDetails.Find(id);
        }

        public void AddSaleOrderDetail(SaleOrderDetail saleOrderDetail)
        {
            _context.SaleOrderDetails.Add(saleOrderDetail);
        }

        public void UpdateSaleOrderDetail(int id, SaleOrderDetail saleOrderDetail)
        {
            _context.SaleOrderDetails.Attach(saleOrderDetail);
            _context.Entry(saleOrderDetail).State = EntityState.Modified;
        }

        public void DeleteSaleOrderDetail(int id)
        {
            var saleOrderDetail = new SaleOrderDetail { Id = id };
            _context.SaleOrderDetails.Attach(saleOrderDetail);
            _context.Entry(saleOrderDetail).State = EntityState.Deleted;
        }
    }
}
using System.Collections.Generic;
using POSApp.Core.Models;

namespace POSApp.Core.Repositories
{
   public interface ISaleOrderRepository
    {
        IEnumerable<SaleOrder> GetSaleOrders();
        SaleOrder GetSaleOrderById(int id);
        void AddSaleOrder(SaleOrder saleOrder);
        void UpdateSaleOrder(int id, SaleOrder saleOrder);
        void DeleteSaleOrder(int id);
    }
}

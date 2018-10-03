using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoSCloudApp.Core.Models;

namespace PoSCloudApp.Core.Repositories
{
   public interface ISaleOrderRepository
    {
        IEnumerable<SaleOrder> GetSaleOrders();
        SaleOrder GetSaleOrderById(int id);
        void UpdateSaleOrder(int id, SaleOrder saleOrder);
        void DeleteSaleOrder(int id);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoSCloudApp.Core.Models;

namespace PoSCloudApp.Core.Repositories
{
    public interface IPurchaseOrderRepository
    {
        IEnumerable<PurchaseOrder> GetPurchaseOrders();
        PurchaseOrder GetPurchaseOrderById(int id);
        void UpdatePurchaseOrder(int id, PurchaseOrder purchaseOrder);
        void DeletePurchaseOrder(int id);
    }
}

using System.Collections.Generic;
using POSApp.Core.Models;

namespace POSApp.Core.Repositories
{
    public interface IPurchaseOrderRepository
    {
        IEnumerable<PurchaseOrder> GetPurchaseOrders();
        PurchaseOrder GetPurchaseOrderById(int id);
        void AddPurchaseOrder(PurchaseOrder purchaseOrder);
        void UpdatePurchaseOrder(int id, PurchaseOrder purchaseOrder);
        void DeletePurchaseOrder(int id);
        IEnumerable<PurchaseOrder> GetApiPurchaseOrders();
    }
}

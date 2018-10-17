using System.Collections.Generic;
using POSApp.Core.Models;

namespace POSApp.Core.Repositories
{
   public interface IPurchaseOrderDetailRepository
   {
       IEnumerable<PurchaseOrderDetail> GetPurchaseOrderDetails(int purchaseOrderId);
       PurchaseOrderDetail GetPurchaseOrderDetailById(int id);
       void AddPurchaseOrderDetail(PurchaseOrderDetail purchaseOrderDetail);
       void UpdatePurchaseOrderDetail(int id, PurchaseOrderDetail purchaseOrderDetail);
       void DeletePurchaseOrderDetail(int id);
   }
}

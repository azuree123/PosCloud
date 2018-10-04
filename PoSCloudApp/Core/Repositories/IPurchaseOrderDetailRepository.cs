using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoSCloudApp.Core.Models;

namespace PoSCloudApp.Core.Repositories
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

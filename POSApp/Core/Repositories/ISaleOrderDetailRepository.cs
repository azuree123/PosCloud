using System.Collections.Generic;
using POSApp.Core.Models;

namespace POSApp.Core.Repositories
{
    public interface ISaleOrderDetailRepository
    {
        IEnumerable<SaleOrderDetail> GetSaleOrderDetails(int saleOrderId);
        SaleOrderDetail GetSaleOrderDetailById(int id);
        void AddSaleOrderDetail(SaleOrderDetail saleOrderDetail);
        void UpdateSaleOrderDetail(int id, SaleOrderDetail saleOrderDetail);
        void DeleteSaleOrderDetail(int id);
    }
}

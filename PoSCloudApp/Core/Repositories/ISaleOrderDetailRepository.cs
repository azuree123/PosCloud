using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoSCloudApp.Core.Models;

namespace PoSCloudApp.Core.Repositories
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

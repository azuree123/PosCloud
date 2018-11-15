using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POSApp.Core.ViewModels;

namespace POSApp.Core.Repositories
{
   public interface IReportsRepository
   {
       List<TransDetailViewModel> GenerateProductSalesData(int storeId, DateTime dateFrom, DateTime dateTo);
       List<CategoryReportViewModel> GenerateCategoriesSalesData(int storeId, DateTime dateFrom, DateTime dateTo);
       List<ProductSizeSaleViewModel> GenerateProductSizeWiseSalesData(int storeId, DateTime dateFrom, DateTime dateTo);
       List<ComboReportSaleViewModel> GenerateComboSalesData(int storeId, DateTime dateFrom, DateTime dateTo);
       List<BranchReportSaleViewModel> GenerateBranchSalesData(int storeId, DateTime dateFrom, DateTime dateTo);
   }
}

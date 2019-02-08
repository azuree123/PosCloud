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
       List<ProductSalesReportViewModel> GenerateProductSalesData(int storeId, DateTime dateFrom, DateTime dateTo);
       List<CategoryReportViewModel> GenerateCategoriesSalesData(int storeId, DateTime dateFrom, DateTime dateTo);
       List<ProductSizeSaleViewModel> GenerateProductSizeWiseSalesData(int storeId, DateTime dateFrom, DateTime dateTo);
       List<ComboReportSaleViewModel> GenerateComboSalesData(int storeId, DateTime dateFrom, DateTime dateTo);
       List<BranchReportSaleViewModel> GenerateBranchSalesData(int storeId, DateTime dateFrom, DateTime dateTo);
       List<SalesReportViewModel> GenerateSalesData(int storeId, DateTime dateFrom, DateTime dateTo);
       List<ProductSizeOrderTypeReportViewModel> GenerateProductSizeOrderTypeSalesData(int storeId, DateTime dateFrom, DateTime dateTo);
       List<OrderTypeSaleReportViewModel> GenerateProductOrderTypeSalesData(int storeId, DateTime dateFrom, DateTime dateTo);
       List<PaymentMethodReportViewModel> GeneratePaymentMethodWiseSalesData(int storeId, DateTime dateFrom, DateTime dateTo);
       List<DineTableReportViewModel> GenerateTableWiseSalesData(int storeId, DateTime dateFrom, DateTime dateTo);
       List<ProductTimelySaleViewModel> GenerateProductTimeWiseSalesData(int storeId, DateTime dateFrom,DateTime dateTo);
       List<BranchTimelyReportViewModel> GenerateTimelyBranchSalesData(int storeId, DateTime dateFrom, DateTime dateTo);
       List<CustomerSaleReportViewModel> GenerateCustomerWiseSalesData(int storeId, DateTime dateFrom, DateTime dateTo);
       List<ModifierReportViewModel> GenerateModifierSalesData(int storeId, DateTime dateFrom, DateTime dateTo);
       List<ModifierReportViewModel> GenerateProductModifierSalesData(int storeId, DateTime dateFrom, DateTime dateTo);
       List<RecipeReportViewModel> GenerateProductRecipeData(int storeId, DateTime dateFrom, DateTime dateTo);
       List<StockReportViewModel> GenerateStockData(int storeId, DateTime dateFrom, DateTime dateTo);
       decimal GetProductSalesDiscount(int storeId, DateTime dateFrom, DateTime dateTo);
   }
}

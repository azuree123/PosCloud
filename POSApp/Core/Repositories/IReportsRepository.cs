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
       List<StockTakingReportViewModel> GenerateStockTakingData(List<int> storeIds, DateTime dateFrom, DateTime dateTo);
       List<TransferReportViewModel> GenerateTransferData(List<int> storeIds, DateTime dateFrom, DateTime dateTo);
       List<ProductSalesReportViewModel> GenerateProductSalesData(List<int> categoryIds, List<string> sizes, List<int> storeIds, DateTime dateFrom, DateTime dateTo);
       List<CategoryReportViewModel> GenerateCategoriesSalesData(List<int> storeIds, DateTime dateFrom, DateTime dateTo);
       List<ProductSizeSaleViewModel> GenerateProductSizeWiseSalesData(List<int> storeIds, DateTime dateFrom, DateTime dateTo);
       List<ComboReportSaleViewModel> GenerateComboSalesData(List<int> storeIds, DateTime dateFrom, DateTime dateTo);
       List<BranchReportSaleViewModel> GenerateBranchSalesData(List<int> storeIds, DateTime dateFrom, DateTime dateTo);

       List<SalesReportViewModel> GenerateSalesData(List<int> storeIds, List<string> methods, List<string> orderTypes,
           List<int> customers, DateTime dateFrom, DateTime dateTo);
        List<ProductSizeOrderTypeReportViewModel> GenerateProductSizeOrderTypeSalesData(List<int> storeIds, DateTime dateFrom, DateTime dateTo);
       List<OrderTypeSaleReportViewModel> GenerateProductOrderTypeSalesData(List<int> storeIds, DateTime dateFrom, DateTime dateTo);
       List<PaymentMethodReportViewModel> GeneratePaymentMethodWiseSalesData(List<int> storeIds, DateTime dateFrom, DateTime dateTo);
       List<DineTableReportViewModel> GenerateTableWiseSalesData(List<int> storeIds, List<int> tableIds,DateTime dateFrom, DateTime dateTo);
       List<ProductTimelySaleViewModel> GenerateProductTimeWiseSalesData(List<int> storeIds, DateTime dateFrom,DateTime dateTo);
       List<BranchTimelyReportViewModel> GenerateTimelyBranchSalesData(List<int> storeIds, DateTime dateFrom, DateTime dateTo);
       List<CustomerSaleReportViewModel> GenerateCustomerWiseSalesData(List<int> storeIds, DateTime dateFrom, DateTime dateTo);
       List<ModifierReportViewModel> GenerateModifierSalesData(List<int> storeIds, DateTime dateFrom, DateTime dateTo);
       List<ModifierReportViewModel> GenerateProductModifierSalesData(List<int> storeIds, DateTime dateFrom, DateTime dateTo);
       List<RecipeReportViewModel> GenerateProductRecipeData(List<int> storeIds, List<string> productCodes);
       List<StockReportViewModel> GenerateStockData(List<int> storeIds,List<string> productCodes);
       decimal GetProductSalesDiscount(List<int> storeIds, DateTime dateFrom, DateTime dateTo);
       List<AgentIncomeReportViewModel> GenerateEmployeeIncomeData(List<int> storeIds, List<int> designationIds);
       List<BatchWiseExpReportViewModel> BatchWiseExpiryData(List<int> storeIds, DateTime dateFrom, DateTime dateTo);
       List<PaymentMethodTimelySaleViewModel> GeneratePaymentMethodTimeWiseSalesData(List<int> storeIds, DateTime dateFrom,
           DateTime dateTo);

       List<SubReportViewModel> GenerateSubReportData(int storeId, string details, string reportName);

       List<PurchaseReportViewModel> GeneratePurchaseOrderData(List<int> storeIds, DateTime dateFrom, DateTime dateTo,
           List<int> supplierId);

       List<StockReportViewModel> GenerateProductsStockData(int storeId);
       List<PurchaseReportViewModel> GeneratePurchasesPerSupplierData(List<int> storeIds, int supplierId);
       List<SalesReportViewModel> GenerateTransactionsData(List<int> storeIds, DateTime dateFrom, DateTime dateTo);
       List<BusinessPartnerViewModel> GenerateCustomersData(List<int> storeIds);
       List<ProductCostReportViewModel> GenerateProductCostData(List<int> storeIds, List<string> productCodes);

       List<EmployeeShiftReportViewModel> GenerateEmployeeShiftData(List<int> storeIds, List<int> employeeIds,
           List<int> shiftIds);
       List<TillOperationReportViewModel> GenerateTillOperationData(List<int> storeIds, DateTime dateFrom, DateTime dateTo);
       List<VoidReasonsReportViewModel> GenerateVoidReasonsData(List<int> storeIds, DateTime dateFrom, DateTime dateTo);
       List<SalesReportViewModel> GenerateConsumptionData(List<int> storeIds, DateTime dateFrom, DateTime dateTo);
       List<ReturnReportViewModel> GenerateReturnData(List<int> storeIds, DateTime dateFrom, DateTime dateTo);
       List<TaxReportViewModel> GenerateTaxesData(List<int> storeIds, DateTime dateFrom, DateTime dateTo);
       List<OrderDiscountViewModel> GenerateOrderDiscountData(List<int> storeIds, DateTime dateFrom, DateTime dateTo);
       List<ProductDiscountViewModel> GenerateProductDiscountData(List<int> storeIds, DateTime dateFrom, DateTime dateTo);
       List<InventoryCostReportViewModel> GenerateItemsCostData(List<int> storeIds, List<string> productCodes,DateTime dateFrom, DateTime dateTo);
       List<ExpirationReportViewModel> GenerateExpiryData(List<int> storeIds, DateTime dateFrom, DateTime dateTo);
       List<WarehouseStockReportViewModel> GenerateWarehouseStock(List<int> warehouseIds, List<string> productCodes);
   }
}

﻿using System;
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
       List<StockReportViewModel> GenerateStockData(int storeId);
       decimal GetProductSalesDiscount(int storeId, DateTime dateFrom, DateTime dateTo);
       List<AgentIncomeReportViewModel> GenerateEmployeeIncomeData(int storeId, int designationId);

       List<PaymentMethodTimelySaleViewModel> GeneratePaymentMethodTimeWiseSalesData(int storeId, DateTime dateFrom,
           DateTime dateTo);

       List<SubReportViewModel> GenerateSubReportData(string details, string reportName);
       List<PurchaseReportViewModel> GenerateTotalPurchasesData(int storeId, DateTime dateFrom, DateTime dateTo);
       List<PurchaseReportViewModel> GeneratePurchasesPerSupplierData(int storeId, int supplierId);
       List<SalesReportViewModel> GenerateTransactionsData(int storeId, DateTime dateFrom, DateTime dateTo);
       List<BusinessPartnerViewModel> GenerateCustomersData(int storeId);
       List<ProductCostReportViewModel> GenerateProductCostData(int storeId, DateTime dateFrom, DateTime dateTo);
       List<EmployeeShiftReportViewModel> GenerateEmployeeShiftData(int storeId, DateTime dateFrom, DateTime dateTo);
       List<TillOperationReportViewModel> GenerateTillOperationData(int storeId, DateTime dateFrom, DateTime dateTo);
       List<VoidReasonsReportViewModel> GenerateVoidReasonsData(int storeId, DateTime dateFrom, DateTime dateTo);
       List<SalesReportViewModel> GenerateConsumptionData(int storeId, DateTime dateFrom, DateTime dateTo);
       List<ReturnReportViewModel> GenerateReturnData(int storeId, DateTime dateFrom, DateTime dateTo);
       List<TaxReportViewModel> GenerateTaxesData(int storeId, DateTime dateFrom, DateTime dateTo);
       List<OrderDiscountViewModel> GenerateOrderDiscountData(int storeId, DateTime dateFrom, DateTime dateTo);
       List<ProductDiscountViewModel> GenerateProductDiscountData(int storeId, DateTime dateFrom, DateTime dateTo);
       List<InventoryCostReportViewModel> GenerateItemsCostData(int storeId, DateTime dateFrom, DateTime dateTo);
       List<ExpiryReportViewModel> GenerateExpiryData(int storeId, DateTime dateFrom, DateTime dateTo);
   }
}

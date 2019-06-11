
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using POSApp.Models;
using POSApp.Persistence;
using Newtonsoft.Json;

namespace POSApp.SecurityFilters
{
    public class Config
    {
        

        public enum SecurityRights
        {
            SaleOrders = 1,
            PurchaseOrders = 2,
            Sales = 3,
            Expense = 4,
            Products=5,
            Setup=6,
            OpeningStock=7,
            PointOfSale=8,
            Reports = 9,
            StockTaking = 10,
            ExcelImport= 11,
            Device = 12,
            Account = 13,
            Store = 14,
            User = 15
        }
 

        public enum SaleOrders
        {
            DailySales = 1,
            DailyFunds = 2,
            MIFData = 3,
            PromotionDetails = 4,
            IssueItems=5

           

        }
        public enum PurchaseOrders
        {
            PurchaseOrder = 1,
            Stock = 2,
            Transfer = 3,
            OtherOut = 4,
            OtherIn = 5,
            Expiry = 6,
            Waste = 7,
            Purchasing = 8,
            Damage = 9
         

        }
         public enum Expense
        {
             Expense = 1,
            ExpenseHead = 2
       
        }
         public enum Products
        {
            Products = 1,
            Modifiers = 2,
            Combo = 3,
            Sections=4,
            ProductCategory=5,
            ProductCategoryGroup=6,
            InventoryItems = 7,
            ModifierLinkProduct=8,
            ComboOption=9,
            Recipe=10,

        }
         public enum Setup
        {
            Customer = 1,
            Employee = 2,
            BusinessPartner = 3,
            City = 4,
            State = 5,
            TimedEvent = 6,
            Supplier = 7,
            Department = 8,
            Designation = 9,
            Tax = 10,
            Unit = 11,
            DineTable = 12,
            Floor = 13,
            PosTerminal = 14,
            Shift = 15,
            TillOperation = 16,
            Size = 17,
            WareHouse = 18,
            Roles = 19,
            Location =20,
            Discount=21,
            Client=22,



        }
         public enum OpeningStock
        {
            OpeningStock = 1,
            TransactionItems=2

          
           

        }
         public enum PointOfSale
        {
            PointOfSale = 1
           
           

        }
         public enum Reports
        {
            SalesReport = 1,
            CategorySaleReport = 2,
            ProductSaleReport = 3,
            ProductSizeSaleReport = 4,
            ComboSaleReport = 5,
            ProductSizeByOrderTypeReport = 6,
            ModifierSaleReport = 7,
            ProductModifierSaleReport = 8,
            ProductTimelySaleReport = 9,
            BranchSaleReport = 10,
            BranchTimelySaleReport = 11,
            EmployeesIncomeReport = 12,
            CustomerSaleReport = 13,
            PaymentMethodReport = 14,
            PaymentMethodTimelySaleReport = 15,
            OrderTypeSale = 16,
            InventoryItemsTotalCostReport = 17,
            ConsumptionReport = 18,
            ExpirationReport = 19,
            TotalTransfersReport = 20,
            TotalPurchasesReport = 21,
            ProductsRecipeReport = 22,
            ProductsCostReport = 23,
            ProductReturnsReport = 24,
            TillOperationsReport = 25,
            VoidReasonsReport = 26,
            EmployeeShiftsReport = 27,
            TaxesReport = 28,
            OrderDiscountsReport = 29,
            ProductDiscountsReport = 30,
            CustomerReports = 31,
            InventoryTransactionsReports = 32,
            PurchaseOrdersReports = 33,
            StockReport=34,
            StockTakingReport=35,
            PurchasesPerSupplierReport=36,
            ServiceCostReport=37,
            PromotionCostReport=38,
            ExpensesReport=39,
            ServiceSalesReport=40,
            ServiceDurationSalesReport=41,
            ServiceTimelySalesReport=42,
            PromotionTimelySalesReport=43,
            EmployeeMonthlyIncomeReport=44,
            EmployeeCommissionReport=45,
            TableSalesReport= 46,
            OrderTypeSaleReport=47,
        }
         public enum StockTaking
        {
            StockTaking = 1
            

        }


        public enum ExcelImport
        {
            DesignationExcelImport = 1,
            StateExcelImport = 2,
            CityExcelImport = 3,
            TaxExcelImport = 4,
            DiscountExcelImport = 5,
            CouponExcelImport = 6,
            CustomerExcelImport = 7,
            DepartmentExcelImport = 8,
            EmployeeExcelImport = 9,
            ExpenseExcelImport = 10,
            ExpenseHeadExcelImport = 11,
            EmployeesIncomeReport = 12,
            StoreExcelImport = 13,
            SupplierExcelImport = 14,
            ProductExcelImport = 15,
            ProductCategoryExcelImport = 16,
            ProductCategoryGroupExcelImport = 17,
            DeviceExcelImport = 18,
            FloorExcelImport = 19,
            DineTableExcelImport = 20,
            ClientExcelImport = 21,
            UnitExcelImport = 22,
            SectionExcelImport = 23,
            ShiftExcelImport = 24,
            SizeExcelImport = 25
          
        }
        public enum Device
        {
            Device = 1


        }

        public enum Account
        {
            Register = 1


        }
        public enum Store
        {
            Store = 1


        }
        public enum User
        {
            User = 1,
            SecurityObject = 2

        }




    }

    public static class UserData
    {
        public static UserRoleDataViewModel GetUserRoleData(HttpContextBase httpContext)
        {
            var cookie = HttpContext.Current.Request.Cookies["UserRoleData"];
            string val = string.Empty;
            if (cookie != null)
            {
                val = AuthHelper.Decrypt(cookie.Value);
            }
            return JsonConvert.DeserializeObject<UserRoleDataViewModel>(val);
        }
    }
}
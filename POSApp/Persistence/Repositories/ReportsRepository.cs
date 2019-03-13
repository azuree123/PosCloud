using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using AutoMapper;
using POSApp.Core.Repositories;
using POSApp.Core.ViewModels;

namespace POSApp.Persistence.Repositories
{
    public class ReportsRepository:IReportsRepository
    {
        private PosDbContext _context;

        public ReportsRepository(PosDbContext context)
        {
            _context = context;
        }
        public List<SubReportViewModel> GenerateSubReportData(string details,string reportName)
        {
            var sql = @"select a.Name as CompanyName,a.Contact as PhoneNumber,a.Address as Address
			from PosCloud.Clients as a 
            inner join PosCloud.Stores as d on a.Id=d.ClientId
            group by a.Name,a.Contact,a.Address
                ";
            var data = _context.Database.SqlQuery<SubReportViewModel>(sql).ToList();
            foreach (var subReportViewModel in data)
            {
                subReportViewModel.ReportName = reportName;
                subReportViewModel.Details = details;
            }
            return data;
        }
        public List<ProductSalesReportViewModel> GenerateProductSalesData(int storeId,DateTime dateFrom,DateTime dateTo)
        {
            var parameters = new List<SqlParameter> { new SqlParameter("@p1", storeId), new SqlParameter("@p2", dateFrom), new SqlParameter("@p3", dateTo) };
            var sql = @"select CONVERT(date,a.TransDate) as Date,c.Name as ProductName,SUM(b.Quantity) as Quantity,c.CostPrice as CostPrice
            ,b.UnitPrice as UnitPrice,b.Tax as Tax,b.Discount as Discount 
			from PosCloud.TransMaster as a 
            inner join PosCloud.TransDetails as b on a.id=b.TransMasterId AND a.StoreId=b.StoreId
            inner join PosCloud.Products as c on b.ProductCode=c.ProductCode AND b.StoreId=c.StoreId  
            
           where a.StoreId=@p1 and a.TransDate >=@p2 and a.TransDate<=@p3 and c.InventoryItem = '0'
            group by  c.Name,b.Tax,b.Discount,c.CostPrice,b.UnitPrice,CONVERT(date,a.TransDate)
                ";
            var data= _context.Database.SqlQuery<ProductSalesReportViewModel>(sql, parameters.ToArray()).ToList();
            return data;
        }
        public List<AgentIncomeReportViewModel> GenerateEmployeeIncomeData(int storeId, int designationId)
        {
            var parameters = new List<SqlParameter> { new SqlParameter("@p1", storeId), new SqlParameter("@p2", designationId) };
            var sql = @"select a.Name as Name,c.Name as Designation,SUM(a.Salary) as Salary,SUM(a.Commission) as Commission,SUM(a.Salary+a.Commission)as Income 
            
			from PosCloud.Employees as a 
            inner join PosCloud.Designations as c on a.DesignationId  = c.Id
			inner join PosCloud.Stores as d on a.StoreId=d.Id
            where a.StoreId = @p1 and a.DesignationId = @p2
            group by a.Name,c.Name
                ";
            var data = _context.Database.SqlQuery<AgentIncomeReportViewModel>(sql, parameters.ToArray()).ToList();
            return data;
        }
        public decimal GetProductSalesDiscount(int storeId, DateTime dateFrom, DateTime dateTo)
        {
            var parameters = new List<SqlParameter> { new SqlParameter("@p1", storeId), new SqlParameter("@p2", dateFrom), new SqlParameter("@p3", dateTo) };
            var sql = @"select Sum(Discount)
                from PosCloud.TransMaster 
           where StoreId=@p1 and TransDate >=@p2 and TransDate<=@p3
                ";
            var data = _context.Database.SqlQuery<decimal>(sql, parameters.ToArray()).Sum();
            return data;
        }


        public List<CategoryReportViewModel> GenerateCategoriesSalesData(int storeId, DateTime dateFrom, DateTime dateTo)
        {
            var parameters = new List<SqlParameter> { new SqlParameter("@p1", storeId), new SqlParameter("@p2", dateFrom), new SqlParameter("@p3", dateTo) };
            var sql = @"select d.Name as CategoryName,SUM(b.Quantity) as Qty,SUM(b.UnitPrice*b.Quantity)as Amount,a.TransDate as Date from PosCloud.TransMaster as a 
            inner join PosCloud.TransDetails as b on a.id=b.TransMasterId
            inner join PosCloud.Products as c on b.ProductCode=c.ProductCode 
            inner join PosCloud.ProductCategories as d on c.CategoryId=d.Id
            where a.StoreId=@p1 and a.TransDate >=@p2 and a.TransDate<=@p3
            group by d.Name,a.TransDate
                ";

            return _context.Database.SqlQuery<CategoryReportViewModel>(sql, parameters.ToArray()).ToList();
        }
        public List<ProductSizeSaleViewModel> GenerateProductSizeWiseSalesData(int storeId, DateTime dateFrom, DateTime dateTo)
        {
            var parameters = new List<SqlParameter> { new SqlParameter("@p1", storeId), new SqlParameter("@p2", dateFrom), new SqlParameter("@p3", dateTo) };
            var sql = @"select c.Name as ProductName,c.Size as Size,SUM(b.Quantity) as Qty,SUM(b.UnitPrice*b.Quantity)as Amount,a.TransDate as Date from PosCloud.TransMaster as a 
            inner join PosCloud.TransDetails as b on a.id=b.TransMasterId
            inner join PosCloud.Products as c on b.ProductCode=c.ProductCode 
            where a.StoreId=@p1 and a.TransDate >=@p2 and a.TransDate<=@p3 and c.InventoryItem ='0'
            group by c.Name,c.Size,a.TransDate
                ";

            return _context.Database.SqlQuery<ProductSizeSaleViewModel>(sql, parameters.ToArray()).ToList();
        }
        public List<ProductSizeOrderTypeReportViewModel> GenerateProductSizeOrderTypeSalesData(int storeId, DateTime dateFrom, DateTime dateTo)
        {
            var parameters = new List<SqlParameter> { new SqlParameter("@p1", storeId), new SqlParameter("@p2", dateFrom), new SqlParameter("@p3", dateTo) };
            var sql = @"select c.Name as ProductName,a.DeliveryType as OrderType,c.Size as Size,SUM(b.Quantity) as Qty,SUM(b.UnitPrice*b.Quantity)as Amount,a.TransDate as Date from PosCloud.TransMaster as a 
            inner join PosCloud.TransDetails as b on a.id=b.TransMasterId
			
            inner join PosCloud.Products as c on b.ProductCode=c.ProductCode 
            where a.StoreId=@p1 and a.TransDate >=@p2 and a.TransDate<=@p3 and c.InventoryItem ='0'
            group by c.Name,c.Size,a.DeliveryType,a.TransDate
                ";

            return _context.Database.SqlQuery<ProductSizeOrderTypeReportViewModel>(sql, parameters.ToArray()).ToList();
        }
        public List<OrderTypeSaleReportViewModel> GenerateProductOrderTypeSalesData(int storeId, DateTime dateFrom, DateTime dateTo)
        {
            var parameters = new List<SqlParameter> { new SqlParameter("@p1", storeId), new SqlParameter("@p2", dateFrom), new SqlParameter("@p3", dateTo) };
            var sql = @"select a.DeliveryType as OrderType,a.TransCode as InvoiceNumber,SUM(b.Quantity) as Qty,SUM(b.UnitPrice*b.Quantity)as Amount,a.Discount,a.Tax,a.TransDate as Date from PosCloud.TransMaster as a 
            inner join PosCloud.TransDetails as b on a.id=b.TransMasterId
            inner join PosCloud.Products as c on b.ProductCode=c.ProductCode 
            where a.StoreId=@p1 and a.TransDate >=@p2 and a.TransDate<=@p3 and c.InventoryItem = '0'
            group by a.TransCode,a.DeliveryType,a.TransDate,a.Discount,a.Tax
                ";

            return _context.Database.SqlQuery<OrderTypeSaleReportViewModel>(sql, parameters.ToArray()).ToList();
        }
        public List<ComboReportSaleViewModel> GenerateComboSalesData(int storeId, DateTime dateFrom, DateTime dateTo)
        {
            var parameters = new List<SqlParameter> { new SqlParameter("@p1", storeId), new SqlParameter("@p2", dateFrom), new SqlParameter("@p3", dateTo) };
            var sql = @"select c.Name as ComboName,SUM(b.Quantity) as Qty,SUM(b.UnitPrice*b.Quantity)as Amount,a.TransDate as Date from PosCloud.TransMaster as a 
            inner join PosCloud.TransDetails as b on a.id=b.TransMasterId
            inner join PosCloud.Products as c on b.ProductCode=c.ProductCode
            where a.StoreId=@p1 and a.TransDate >=@p2 and a.TransDate<=@p3 and c.Type='Combo' and c.InventoryItem ='0'
            group by c.Name,a.TransDate
                ";

            return _context.Database.SqlQuery<ComboReportSaleViewModel>(sql, parameters.ToArray()).ToList();
        }
        public List<ModifierReportViewModel> GenerateModifierSalesData(int storeId, DateTime dateFrom, DateTime dateTo)
        {
            var parameters = new List<SqlParameter> { new SqlParameter("@p1", storeId), new SqlParameter("@p2", dateFrom), new SqlParameter("@p3", dateTo) };
            var sql = @"select e.Name as ModifierName,f.Name as ModifierOption,g.Name as ProductName,a.TransCode as InvoiceNumber,SUM(b.Quantity) as Qty,SUM(b.UnitPrice*b.Quantity)as Amount,a.TransDate as Date 
            ,a.Discount as Discount,a.Tax as Tax
			from PosCloud.TransMaster as a 
            inner join PosCloud.TransDetails as b on a.id=b.TransMasterId
            inner join PosCloud.ModifierTransDetails as c on b.Id = c.TransDetailId
			inner join PosCloud.ModifierOptions as f on c.ModifierOptionId= f.Id
			inner join PosCloud.Modifier as e on f.ModifierId = e.Id
			inner join PosCloud.Products as g on g.ProductCode=b.ProductCode
			where a.StoreId=@p1 and a.TransDate >=@p2 and a.TransDate<=@p3 and g.InventoryItem ='0'
            
            group by e.Name,f.Name,g.Name,a.TransCode,a.TransDate,a.Discount,a.Tax
                ";

            return _context.Database.SqlQuery<ModifierReportViewModel>(sql, parameters.ToArray()).ToList();
        }
        public List<BranchReportSaleViewModel> GenerateBranchSalesData(int storeId, DateTime dateFrom, DateTime dateTo)
        {
            var parameters = new List<SqlParameter> { new SqlParameter("@p1", storeId), new SqlParameter("@p2", dateFrom), new SqlParameter("@p3", dateTo) };
            var sql = @"select d.Name as BranchName,c.Name as ProductName,SUM(b.Quantity) as Qty,SUM(b.UnitPrice*b.Quantity)as Amount,a.TransDate as Date 
			from PosCloud.TransMaster as a 
            inner join PosCloud.TransDetails as b on a.id=b.TransMasterId
            inner join PosCloud.Products as c on b.ProductCode=c.ProductCode
			inner join PosCloud.Stores as d on a.StoreId=d.Id
            where a.StoreId=@p1 and a.TransDate >=@p2 and a.TransDate<=@p3 and c.InventoryItem = '0'
            group by d.Name,c.Name,a.TransDate
            
                ";

            return _context.Database.SqlQuery<BranchReportSaleViewModel>(sql, parameters.ToArray()).ToList();
        }
        public List<SalesReportViewModel> GenerateSalesData(int storeId, DateTime dateFrom, DateTime dateTo)
        {
            var parameters = new List<SqlParameter> { new SqlParameter("@p1", storeId), new SqlParameter("@p2", dateFrom), new SqlParameter("@p3", dateTo) };
            var sql = @"select d.Name as BranchName,a.TransCode as InvoiceNumber,SUM(b.Quantity) as Qty,SUM(b.UnitPrice*b.Quantity)as Amount,a.TransDate as Date 
            ,a.Discount as Discount,a.Tax as Tax
			from PosCloud.TransMaster as a 
            inner join PosCloud.TransDetails as b on a.id=b.TransMasterId
			inner join PosCloud.Stores as d on a.StoreId=d.Id
            where a.StoreId=@p1 and a.TransDate >=@p2 and a.TransDate<=@p3 and Type= 'INV' and ((a.TransStatus = 'Paid' or a.TransStatus = 'Complete') or a.TransStatus = 'Complete')
            group by d.Name,a.TransCode,a.TransCode,a.TransDate,a.Discount,a.Tax
            
            
            
                ";

            return _context.Database.SqlQuery<SalesReportViewModel>(sql, parameters.ToArray()).ToList();
        }
        public List<PaymentMethodReportViewModel> GeneratePaymentMethodWiseSalesData(int storeId, DateTime dateFrom, DateTime dateTo)
        {
            var parameters = new List<SqlParameter> { new SqlParameter("@p1", storeId), new SqlParameter("@p2", dateFrom), new SqlParameter("@p3", dateTo) };
            var sql = @"select d.Method as PaymentMethod,a.TransCode as InvoiceNumber,SUM(b.Quantity) as Qty,SUM(b.UnitPrice*b.Quantity)as Amount,a.Discount,a.Tax,a.TransDate as Date 
            
			from PosCloud.TransMaster as a 
            inner join PosCloud.TransDetails as b on a.id=b.TransMasterId
			inner join PosCloud.TransMasterPaymentMethods as d on a.Id=d.TransMasterId
			inner join PosCloud.Products as c on b.ProductCode=c.ProductCode
            where a.StoreId=@p1 and a.TransDate >=@p2 and a.TransDate<=@p3 
            group by  d.Method,a.Discount,a.Tax,a.TransCode,a.TransDate
            
                ";

            return _context.Database.SqlQuery<PaymentMethodReportViewModel>(sql, parameters.ToArray()).ToList();
        }
        public List<DineTableReportViewModel> GenerateTableWiseSalesData(int storeId, DateTime dateFrom, DateTime dateTo)
        {
            var parameters = new List<SqlParameter> { new SqlParameter("@p1", storeId), new SqlParameter("@p2", dateFrom), new SqlParameter("@p3", dateTo) };
            var sql = @"select f.DineTableNumber as TableNumber,a.TransCode as InvoiceNumber,SUM(b.Quantity) as Qty,SUM(b.UnitPrice*b.Quantity)as Amount,a.TransDate as Date 
            ,a.Discount as Discount,a.Tax as Tax
			from PosCloud.TransMaster as a 
            inner join PosCloud.TransDetails as b on a.id=b.TransMasterId
            inner join PosCloud.DineTables as f on a.DineTableId = f.Id
            where a.StoreId=@p1 and a.TransDate >=@p2 and a.TransDate<=@p3 
            group by f.DineTableNumber,a.TransCode,a.TransDate,a.Discount,a.Tax
            
                ";

            return _context.Database.SqlQuery<DineTableReportViewModel>(sql, parameters.ToArray()).ToList();
        }
        public List<ProductTimelySaleViewModel> GenerateProductTimeWiseSalesData(int storeId, DateTime dateFrom, DateTime dateTo)
        {
            var parameters = new List<SqlParameter> { new SqlParameter("@p1", storeId), new SqlParameter("@p2", dateFrom), new SqlParameter("@p3", dateTo) };
            var sql = @"select CONVERT(time(0),(CONVERT(VARCHAR(2), a.TransDate, 108))+':00') as Time,c.Name as ProductName,SUM(b.Quantity) as Quantity,SUM(b.UnitPrice*b.Quantity)as UnitPrice,b.Discount as Discount from PosCloud.TransMaster as a 
            inner join PosCloud.TransDetails as b on a.id=b.TransMasterId
            inner join PosCloud.Products as c on b.ProductCode=c.ProductCode  
            where a.StoreId=@p1 and a.TransDate >=@p2 and a.TransDate<=@p3 and c.InventoryItem ='0'
            group by  c.Name,b.Discount,CONVERT(time(0),(CONVERT(VARCHAR(2), a.TransDate, 108)+':00'))
            
                ";
            var data = _context.Database.SqlQuery<ProductTimelySaleViewModel>(sql, parameters.ToArray()).ToList();
            return data;
        }
        public List<PaymentMethodTimelySaleViewModel> GeneratePaymentMethodTimeWiseSalesData(int storeId, DateTime dateFrom, DateTime dateTo)
        {
            var parameters = new List<SqlParameter> { new SqlParameter("@p1", storeId), new SqlParameter("@p2", dateFrom), new SqlParameter("@p3", dateTo) };
            var sql = @"select CONVERT(time(0),(CONVERT(VARCHAR(2), a.TransDate, 108))+':00') as Time,d.Method as PaymentMethod,a.TransCode as InvoiceNumber,SUM(b.Quantity) as Qty,SUM(b.UnitPrice*b.Quantity)as Amount,a.Discount,a.Tax 
            
			from PosCloud.TransMaster as a 
            inner join PosCloud.TransDetails as b on a.id=b.TransMasterId
			inner join PosCloud.TransMasterPaymentMethods as d on a.Id=d.TransMasterId
			inner join PosCloud.Products as c on b.ProductCode=c.ProductCode
             where a.StoreId=@p1 and a.TransDate >=@p2 and a.TransDate<=@p3
            group by d.Method,a.Discount,a.Tax,a.TransCode,CONVERT(time(0),(CONVERT(VARCHAR(2), a.TransDate, 108)+':00')) 
            
            
            
                ";
            var data = _context.Database.SqlQuery<PaymentMethodTimelySaleViewModel>(sql, parameters.ToArray()).ToList();
            return data;
        }
        public List<BranchTimelyReportViewModel> GenerateTimelyBranchSalesData(int storeId, DateTime dateFrom, DateTime dateTo)
        {
            var parameters = new List<SqlParameter> { new SqlParameter("@p1", storeId), new SqlParameter("@p2", dateFrom), new SqlParameter("@p3", dateTo) };
            var sql = @"select d.Name as BranchName,c.Name as ProductName,SUM(b.Quantity) as Qty,SUM(b.UnitPrice*b.Quantity)as Amount,CONVERT(time(0),(CONVERT(VARCHAR(2), a.TransDate, 108))+':00') as Time
			from PosCloud.TransMaster as a 
            inner join PosCloud.TransDetails as b on a.id=b.TransMasterId
            inner join PosCloud.Products as c on b.ProductCode=c.ProductCode
			inner join PosCloud.Stores as d on a.StoreId=d.Id
            where a.StoreId=@p1 and a.TransDate >=@p2 and a.TransDate<=@p3 and c.InventoryItem ='0'
            group by d.Name,c.Name,CONVERT(time(0),(CONVERT(VARCHAR(2), a.TransDate, 108)+':00'))
                ";
            var data = _context.Database.SqlQuery<BranchTimelyReportViewModel>(sql, parameters.ToArray()).ToList();
            return data;
        }
        public List<CustomerSaleReportViewModel> GenerateCustomerWiseSalesData(int storeId, DateTime dateFrom, DateTime dateTo)
        {
            var parameters = new List<SqlParameter> { new SqlParameter("@p1", storeId), new SqlParameter("@p2", dateFrom), new SqlParameter("@p3", dateTo) };
            var sql = @"select f.Name as CustomerName,a.TransCode as InvoiceNumber,SUM(b.Quantity) as Qty,SUM(b.UnitPrice*b.Quantity)as Amount,a.TransDate as Date 
            ,a.Discount as Discount,a.Tax as Tax
			from PosCloud.TransMaster as a 
            inner join PosCloud.TransDetails as b on a.id=b.TransMasterId
            inner join PosCloud.BusinessPartners as f on a.BusinessPartnerId = f.Id
            where a.StoreId=@p1 and a.TransDate >=@p2 and a.TransDate<=@p3 and f.Type = 'C' and a.Type = 'INV'
             group by f.Name,a.TransCode,a.TransDate,a.Discount,a.Tax
                ";
            var data = _context.Database.SqlQuery<CustomerSaleReportViewModel>(sql, parameters.ToArray()).ToList();
            return data;
        }
        public List<ModifierReportViewModel> GenerateProductModifierSalesData(int storeId, DateTime dateFrom, DateTime dateTo)
        {
            var parameters = new List<SqlParameter> { new SqlParameter("@p1", storeId), new SqlParameter("@p2", dateFrom), new SqlParameter("@p3", dateTo) };
            var sql = @"select g.Name as ProductName,e.Name as ModifierName,f.Name as ModifierOption,a.TransCode as InvoiceNumber,SUM(b.Quantity) as Qty,SUM(b.UnitPrice*b.Quantity)as Amount,a.TransDate as Date 
            ,a.Discount as Discount,a.Tax as Tax
			from PosCloud.TransMaster as a 
            inner join PosCloud.TransDetails as b on a.id=b.TransMasterId
            inner join PosCloud.ModifierTransDetails as c on b.Id = c.TransDetailId
			inner join PosCloud.ModifierOptions as f on c.ModifierOptionId= f.Id
			inner join PosCloud.Modifier as e on f.ModifierId = e.Id
			inner join PosCloud.Products as g on g.ProductCode=b.ProductCode
			where a.StoreId=@p1 and a.TransDate >=@p2 and a.TransDate<=@p3
            group by e.Name,f.Name,g.Name,a.TransCode,a.TransDate,a.Discount,a.Tax
                ";

            return _context.Database.SqlQuery<ModifierReportViewModel>(sql, parameters.ToArray()).ToList();
        }

        public List<RecipeReportViewModel> GenerateProductRecipeData(int storeId, DateTime dateFrom, DateTime dateTo)
        {
            var parameters = new List<SqlParameter> { new SqlParameter("@p1", storeId) };
            var sql = @"SELECT t.Id, t.IngredientCode, t.Quantity, t.ExpiryDate,t1.Name AS ProductName, t2.Name AS IngredientName,t.Calories , t3.Name as Unit
                         FROM  PosCloud.Recipes AS t INNER JOIN
                         PosCloud.Products AS t1 ON t1.ProductCode = t.ProductCode AND t1.StoreId = t.StoreId  INNER JOIN
                         PosCloud.Products AS t2 ON t2.ProductCode = t.IngredientCode AND t2.StoreId = t.StoreId  INNER JOIN
                         PosCloud.Units AS t3 ON t1.UnitId = t3.Id AND t1.StoreId = t3.StoreId 
			             where t.StoreId=@p1;";

            return _context.Database.SqlQuery<RecipeReportViewModel>(sql, parameters.ToArray()).ToList();
        }
        public List<StockReportViewModel> GenerateStockData(int storeId)
        {
            var parameters = new List<SqlParameter> { new SqlParameter("@p1", storeId) };
            var sql = @"Select a.ProductCode,a.Name, ISNULL(
						 (select SUM(r.Quantity) from
						 PosCloud.TransMaster as c inner join PosCloud.TransDetails as d
						 on c.Id=d.TransMasterId AND c.StoreId=d.StoreId
						 inner join PosCloud.Products as e on e.ProductCode=d.ProductCode AND d.StoreId=e.StoreId
						 inner join PosCloud.Recipes as r on r.ProductCode=e.ProductCode AND r.StoreId=e.StoreId
						 inner join PosCloud.Products as t on r.IngredientCode=t.ProductCode AND r.StoreId=t.StoreId	
						 where t.ProductCode=a.ProductCode AND t.StoreId=a.StoreId AND c.Type='OPS' ),0) as OpeningStock,
						 ISNULL(
						 (select SUM(r.Quantity) from
						 PosCloud.TransMaster as c inner join PosCloud.TransDetails as d
						 on c.Id=d.TransMasterId AND c.StoreId=d.StoreId
						 inner join PosCloud.Products as e on e.ProductCode=d.ProductCode AND d.StoreId=e.StoreId
						 inner join PosCloud.Recipes as r on r.ProductCode=e.ProductCode AND r.StoreId=e.StoreId
						 inner join PosCloud.Products as t on r.IngredientCode=t.ProductCode AND r.StoreId=t.StoreId	
						 where t.ProductCode=a.ProductCode AND t.StoreId=a.StoreId AND c.Type='MIF' ),0) as Utilized,
						  ISNULL(
						 (select SUM(d.Quantity) from
						 PosCloud.TransMaster as c inner join PosCloud.TransDetails as d
						 on c.Id=d.TransMasterId AND c.StoreId=d.StoreId
						 where d.ProductCode=a.ProductCode AND d.StoreId=a.StoreId AND c.Type in ('PRI','OTI') ),0) as Purchased,
						 		  ISNULL(
						 (select SUM(d.Quantity) from
						 PosCloud.TransMaster as c inner join PosCloud.TransDetails as d
						 on c.Id=d.TransMasterId AND c.StoreId=d.StoreId
						 where d.ProductCode=a.ProductCode AND d.StoreId=a.StoreId AND c.Type in ('EXP') ),0) as Expired,
						 		  ISNULL(
						 (select SUM(d.Quantity) from
						 PosCloud.TransMaster as c inner join PosCloud.TransDetails as d
						 on c.Id=d.TransMasterId AND c.StoreId=d.StoreId
						 where d.ProductCode=a.ProductCode AND d.StoreId=a.StoreId AND c.Type in ('DMG') ),0) as Damaged,
						 		  ISNULL(
						 (select SUM(d.Quantity) from
						 PosCloud.TransMaster as c inner join PosCloud.TransDetails as d
						 on c.Id=d.TransMasterId AND c.StoreId=d.StoreId
						 where d.ProductCode=a.ProductCode AND d.StoreId=a.StoreId AND c.Type in ('WST') ),0) as Wasted,
						 		  ISNULL(
						 (select SUM(d.Quantity) from
						 PosCloud.TransMaster as c inner join PosCloud.TransDetails as d
						 on c.Id=d.TransMasterId AND c.StoreId=d.StoreId
						 where d.ProductCode=a.ProductCode AND d.StoreId=a.StoreId AND c.Type in ('STI','OTO','TRS') ),0) as Transfered,
						 		  ISNULL(
						 (select SUM(d.Quantity) from
						 PosCloud.TransMaster as c inner join PosCloud.TransDetails as d
						 on c.Id=d.TransMasterId AND c.StoreId=d.StoreId
						 where d.ProductCode=a.ProductCode AND d.StoreId=a.StoreId AND c.Type in ('REF') ),0) as Refunded
						 from PosCloud.Products as a
						 where a.InventoryItem=1 AND a.StoreId=@p1

			            ";

            return _context.Database.SqlQuery<StockReportViewModel>(sql, parameters.ToArray()).ToList();
        }
        public List<PurchaseReportViewModel> GenerateTotalPurchasesData(int storeId, DateTime dateFrom, DateTime dateTo)
        {
            var parameters = new List<SqlParameter> { new SqlParameter("@p1", storeId), new SqlParameter("@p2", dateFrom), new SqlParameter("@p3", dateTo) };
            var sql = @"  select d.Name as BranchName,a.TransCode as InvoiceNumber,SUM(b.Quantity) as Qty,p.PurchaseUnit as Unit,c.Name as SupplierName,SUM(a.TotalPrice+(a.Tax - a.Discount))as Amount 
            
			from PosCloud.TransMaster as a 
			inner join PosCloud.TransDetails as b on a.id=b.TransMasterId
			inner join POsCloud.Products as p on b.ProductCode = p.ProductCode
            inner join PosCloud.BusinessPartners as c on a.BusinessPartnerId  = c.Id
			inner join PosCloud.Stores as d on a.StoreId=d.Id
            where a.StoreId=@p1 and a.TransDate >=@p2 and a.TransDate<=@p3 and a.Type = 'PRO' and c.Type = 'S' 
            group by a.TransCode,c.Name, d.Name,p.PurchaseUnit
                ";

            return _context.Database.SqlQuery<PurchaseReportViewModel>(sql, parameters.ToArray()).ToList();
        }
        public List<PurchaseReportViewModel> GeneratePurchasesPerSupplierData(int storeId, int supplierId)
        {
            var parameters = new List<SqlParameter> { new SqlParameter("@p1", storeId), new SqlParameter("@p2", supplierId) };
            var sql = @" select d.Name as BranchName,a.TransCode as InvoiceNumber,SUM(b.Quantity) as Qty,p.PurchaseUnit as Unit,c.Name as SupplierName,SUM(a.TotalPrice+(a.Tax - a.Discount))as Amount 
            
			from PosCloud.TransMaster as a 
			
			inner join PosCloud.TransDetails as b on a.id=b.TransMasterId
			inner join POsCloud.Products as p on b.ProductCode = p.ProductCode
            inner join PosCloud.BusinessPartners as c on a.BusinessPartnerId  = c.Id
			inner join PosCloud.Stores as d on a.StoreId=d.Id
            where a.Type = 'PRO' and c.Type = 'S' and a.StoreId = @p1 and a.BusinessPartnerId = @p2
            group by a.TransCode,c.Name, d.Name,p.PurchaseUnit

            
           
                ";
            var data = _context.Database.SqlQuery<PurchaseReportViewModel>(sql, parameters.ToArray()).ToList();
            return data;
        }
        public List<SalesReportViewModel> GenerateTransactionsData(int storeId, DateTime dateFrom, DateTime dateTo)
        {
            var parameters = new List<SqlParameter> { new SqlParameter("@p1", storeId), new SqlParameter("@p2", dateFrom), new SqlParameter("@p3", dateTo) };
            var sql = @" select d.Name as BranchName,a.TransCode as InvoiceNumber,c.Name as SupplierName,SUM(a.TotalPrice+(a.Tax - a.Discount))as Amount 
            
			from PosCloud.TransMaster as a 
			
            inner join PosCloud.BusinessPartners as c on a.BusinessPartnerId  = c.Id
			inner join PosCloud.Stores as d on a.StoreId=d.Id
			
            where a.StoreId=@p1 and a.TransDate >=@p2 and a.TransDate<=@p3
            group by a.TransCode,c.Name, d.Name
             
                ";

            return _context.Database.SqlQuery<SalesReportViewModel>(sql, parameters.ToArray()).ToList();
        }
        public List<BusinessPartnerViewModel> GenerateCustomersData(int storeId)
        {
            var parameters = new List<SqlParameter> { new SqlParameter("@p1", storeId)};
            var sql = @" select d.Name as BranchName,a.Name as Name,a.Email as Email,a.PhoneNumber as PhoneNumber,a.Address as Address,a.City as City,a.State as State
            
			from PosCloud.BusinessPartners as a 
			
           
			inner join PosCloud.Stores as d on a.StoreId=d.Id
			
            where a.StoreId=@p1 and a.Type = 'C'

            group by a.Name, d.Name,a.Email,a.PhoneNumber,a.Address,a.City,a.State

			
            
            
             
                ";

            return _context.Database.SqlQuery<BusinessPartnerViewModel>(sql, parameters.ToArray()).ToList();
        }

        public List<ProductCostReportViewModel> GenerateProductCostData(int storeId)
        {
            var parameters = new List<SqlParameter> { new SqlParameter("@p1", storeId) };
            var sql = @" select d.Name as BranchName,a.Name as ProductName,SUM(a.CostPrice) as CostPrice
            
			from PosCloud.Products as a 
			
           
			inner join PosCloud.Stores as d on a.StoreId=d.Id
			
            where a.StoreId=@p1
            group by a.Name, d.Name,a.CostPrice
                ";

            return _context.Database.SqlQuery<ProductCostReportViewModel>(sql, parameters.ToArray()).ToList();
        }

        public List<EmployeeShiftReportViewModel> GenerateEmployeeShiftData(int storeId)
        {
            var parameters = new List<SqlParameter> { new SqlParameter("@p1", storeId) };
            var sql = @" select d.Name as BranchName,a.Name as Shift,e.Name as EmployeeName
            
			from PosCloud.Shifts as a 
			
            inner join PosCloud.Employees as e on a.ShiftId = e.Id
			inner join PosCloud.Stores as d on a.StoreId=d.Id
			
            where a.StoreId=@p1
            group by a.Name, d.Name,e.Name
			
           
            
                ";

            return _context.Database.SqlQuery<EmployeeShiftReportViewModel>(sql, parameters.ToArray()).ToList();
        }
        public List<TillOperationReportViewModel> GenerateTillOperationData(int storeId, DateTime dateFrom, DateTime dateTo)
        {
            var parameters = new List<SqlParameter> { new SqlParameter("@p1", storeId), new SqlParameter("@p2", dateFrom), new SqlParameter("@p3", dateTo) };
            var sql = @"   select t.Name as BranchName,b.Email as UserEmail,a.OperationDate as operationDate, a.OpeningAmount as OpeningAmount,
             a.SystemAmount as SystemAmount,a.PhysicalAmount as PhysicalAmount,s.Name as ShiftName,a.TillOperationType as TillOperationType
             From   PosCloud.TillOperations as a
             inner join PosCloud.Shifts as s on a.ShiftId = s.ShiftId
             inner join AspNetUsers as b on a.ApplicationUserId = b.Id
             inner join PosCloud.Stores as t on a.StoreId = s.StoreId
             where a.StoreId=@p1 and a.OperationDate >= @p2 and a.OperationDate <= @p3
             group by b.Email,a.OperationDate,a.OpeningAmount,a.SystemAmount,a.PhysicalAmount,s.Name,a.TillOperationType,t.Name
                ";

            return _context.Database.SqlQuery<TillOperationReportViewModel>(sql, parameters.ToArray()).ToList();
        }
        public List<SalesReportViewModel> GenerateConsumptionData(int storeId, DateTime dateFrom, DateTime dateTo)
        {
            var parameters = new List<SqlParameter> { new SqlParameter("@p1", storeId), new SqlParameter("@p2", dateFrom), new SqlParameter("@p3", dateTo) };
            var sql = @"  select d.Name as BranchName,a.TransCode as InvoiceNumber,SUM(b.Quantity) as Qty,p.IngredientUnit as IngredientUnit,SUM(b.UnitPrice*b.Quantity)as Amount,a.TransDate as Date 
            ,a.Discount as Discount,a.Tax as Tax
			from PosCloud.TransMaster as a 
            inner join PosCloud.TransDetails as b on a.id=b.TransMasterId
            inner join PosCloud.Products as p on b.ProductCode = p.ProductCode
			inner join PosCloud.Stores as d on a.StoreId=d.Id
            where a.StoreId=@p1 and a.TransDate >=@p2 and a.TransDate<=@p3 and a.Type= 'MIF'
            group by d.Name,a.TransCode,a.TransCode,a.TransDate,a.Discount,a.Tax,p.IngredientUnit
                ";

            return _context.Database.SqlQuery<SalesReportViewModel>(sql, parameters.ToArray()).ToList();
        }
        public List<VoidReasonsReportViewModel> GenerateVoidReasonsData(int storeId, DateTime dateFrom, DateTime dateTo)
        {
            var parameters = new List<SqlParameter> { new SqlParameter("@p1", storeId), new SqlParameter("@p2", dateFrom), new SqlParameter("@p3", dateTo) };
            var sql = @"   select d.Name as BranchName,a.TransCode as TransCode,SUM(b.Quantity) as Qty,SUM(b.UnitPrice*b.Quantity)as Amount,a.TransDate as Date 
            ,a.Description as VoidReasons,p.ProductCode as ProductCode
			from PosCloud.TransMaster as a 
            inner join PosCloud.TransDetails as b on a.id=b.TransMasterId
			inner join PosCloud.Products as p on b.ProductCode = p.ProductCode
			inner join PosCloud.Stores as d on a.StoreId=d.Id
            where a.StoreId=@p1 and a.TransDate >=@p2 and a.TransDate<=@p3 and a.TransStatus = 'Void' 
            group by d.Name,a.TransCode,a.TransDate,a.Description,p.ProductCode
                ";
            return _context.Database.SqlQuery<VoidReasonsReportViewModel>(sql, parameters.ToArray()).ToList();
        }
        public List<ReturnReportViewModel> GenerateReturnData(int storeId, DateTime dateFrom, DateTime dateTo)
        {
            var parameters = new List<SqlParameter> { new SqlParameter("@p1", storeId), new SqlParameter("@p2", dateFrom), new SqlParameter("@p3", dateTo) };
            var sql = @"    select d.Name as BranchName,a.TransCode as InvoiceNumber,SUM(b.Quantity) as Qty,SUM(b.UnitPrice*b.Quantity)as Amount,a.TransDate as Date 
            ,p.Name as Products,a.TransStatus as Status
			from PosCloud.TransMaster as a 
            inner join PosCloud.TransDetails as b on a.id=b.TransMasterId
			inner join PosCloud.Products as p on b.ProductCode = p.ProductCode
			inner join PosCloud.Stores as d on a.StoreId=d.Id
            where a.StoreId=@p1 and a.TransStatus = 'Returned' and a.Type = 'REF' and a.TransDate >=@p2 and a.TransDate<=@p3
            group by d.Name,a.TransCode,a.TransDate,a.Description,p.Name,a.TransStatus
           
                ";
            return _context.Database.SqlQuery<ReturnReportViewModel>(sql, parameters.ToArray()).ToList();
        }

        public List<TaxReportViewModel> GenerateTaxesData(int storeId, DateTime dateFrom, DateTime dateTo)
        {
            var parameters = new List<SqlParameter> { new SqlParameter("@p1", storeId), new SqlParameter("@p2", dateFrom), new SqlParameter("@p3", dateTo) };
            var sql = @"    Select s.Name as BranchName,a.Name as Name, a.Rate,ISNULL(a.IsPercentage,0) as Percentage From PosCloud.Taxes as a 
            inner join PosCloud.Stores as s on a.StoreId = s.Id
            where a.StoreId=@p1 and a.CreatedOn >=@p2 and a.CreatedOn<=@p3
            group by a.Name, a.Rate,a.IsPercentage,s.Name
           
                ";
            return _context.Database.SqlQuery<TaxReportViewModel>(sql, parameters.ToArray()).ToList();
        }

        public List<OrderDiscountViewModel> GenerateOrderDiscountData(int storeId, DateTime dateFrom, DateTime dateTo)
        {
            var parameters = new List<SqlParameter> { new SqlParameter("@p1", storeId), new SqlParameter("@p2", dateFrom), new SqlParameter("@p3", dateTo) };
            var sql = @"select d.Name as BranchName,a.TransCode as InvoiceNumber,a.TransDate as Date 
            ,a.Discount as Discount
			from PosCloud.TransMaster as a 
            inner join PosCloud.TransDetails as b on a.id=b.TransMasterId
			inner join PosCloud.Stores as d on a.StoreId=d.Id
            where a.StoreId=@p1 and Type= 'INV' and (a.TransStatus = 'Paid' or a.TransStatus = 'Complete') and a.TransDate >=@p2 and a.TransDate<=@p3 and a.Discount != 0
            group by d.Name,a.TransCode,a.TransCode,a.TransDate,a.Discount
            
           
                ";
            return _context.Database.SqlQuery<OrderDiscountViewModel>(sql, parameters.ToArray()).ToList();
        }

        public List<ProductDiscountViewModel> GenerateProductDiscountData(int storeId, DateTime dateFrom, DateTime dateTo)
        {
            var parameters = new List<SqlParameter> { new SqlParameter("@p1", storeId), new SqlParameter("@p2", dateFrom), new SqlParameter("@p3", dateTo) };
            var sql = @"select d.Name as BranchName,a.ProductCode as ProductCode,a.Name as ProductName, b.Discount as Discount,t.TransDate as Date
            
			from PosCloud.Products as a 
            inner join PosCloud.TransDetails as b on a.ProductCode=b.ProductCode
			inner join PosCloud.Stores as d on a.StoreId=d.Id
			inner join PosCloud.TransMaster as t on b.TransMasterId = t.Id
            where a.StoreId=@p1 and b.DiscountId is not null and t.TransDate >=@p2 and t.TransDate<=@p3
            group by d.Name,a.Name,b.Discount,a.ProductCode,t.TransDate
            
           
                ";
            return _context.Database.SqlQuery<ProductDiscountViewModel>(sql, parameters.ToArray()).ToList();
        }

        public List<InventoryCostReportViewModel> GenerateItemsCostData(int storeId, DateTime dateFrom, DateTime dateTo)
        {
            var parameters = new List<SqlParameter> { new SqlParameter("@p1", storeId), new SqlParameter("@p2", dateFrom), new SqlParameter("@p3", dateTo) };
            var sql = @"SELECT DISTINCT p.Name as ItemName,p.CostPrice as Cost,CONVERT(date, m.TransDate, 103) as Date
            From PosCloud.Products as p
            inner join PosCloud.TransDetails as t on p.ProductCode = t.ProductCode
            inner join PosCloud.TransMaster as m on t.TransMasterId = m.Id
            inner join PosCloud.Stores as s on p.StoreId = s.Id
            where p.InventoryItem = 1 and p.StoreId=@p1 and m.TransDate >=@p2 and m.TransDate<=@p3
             
            group by p.Name,p.CostPrice,CONVERT(date, m.TransDate, 103)
            
           
                ";
            return _context.Database.SqlQuery<InventoryCostReportViewModel>(sql, parameters.ToArray()).ToList();
        }
        public List<ExpiryReportViewModel> GenerateExpiryData(int storeId, DateTime dateFrom, DateTime dateTo)
        {
            var parameters = new List<SqlParameter> { new SqlParameter("@p1", storeId), new SqlParameter("@p2", dateFrom), new SqlParameter("@p3", dateTo) };
            var sql = @" select d.Name as BranchName,a.TransCode as TransCode,SUM(b.Quantity) as Qty,SUM(b.UnitPrice*b.Quantity)as Amount,a.TransDate as Date 
            ,p.Name as Products
			from PosCloud.TransMaster as a 
            inner join PosCloud.TransDetails as b on a.id=b.TransMasterId
			inner join PosCloud.Products as p on b.ProductCode = p.ProductCode
			inner join PosCloud.Stores as d on a.StoreId=d.Id
             where  p.StoreId=@p1 and a.TransDate >=@p2 and a.TransDate<=@p3 and a.Type = 'EXP'
            group by d.Name,a.TransCode,a.TransDate,p.Name
                ";
            return _context.Database.SqlQuery<ExpiryReportViewModel>(sql, parameters.ToArray()).ToList();
        }
    }
}
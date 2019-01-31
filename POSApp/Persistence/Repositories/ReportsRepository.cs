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

        public List<ProductSalesReportViewModel> GenerateProductSalesData(int storeId,DateTime dateFrom,DateTime dateTo)
        {
            var parameters = new List<SqlParameter> { new SqlParameter("@p1", storeId), new SqlParameter("@p2", dateFrom), new SqlParameter("@p3", dateTo) };
            var sql = @"select CONVERT(date,a.TransDate) as Date,c.Name as ProductName,SUM(b.Quantity) as Quantity,SUM(b.UnitPrice*b.Quantity)as UnitPrice,b.Discount as Discount from PosCloud.TransMaster as a 
            inner join PosCloud.TransDetails as b on a.id=b.TransMasterId
            inner join PosCloud.Products as c on b.ProductCode=c.ProductCode  
           where a.StoreId=@p1 and a.TransDate >=@p2 and a.TransDate<=@p3
            group by  c.Name,b.Discount,CONVERT(date,a.TransDate)
                ";
            var data= _context.Database.SqlQuery<ProductSalesReportViewModel>(sql, parameters.ToArray()).ToList();
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
            where a.StoreId=@p1 and a.TransDate >=@p2 and a.TransDate<=@p3
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
            where a.StoreId=@p1 and a.TransDate >=@p2 and a.TransDate<=@p3
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
            where a.StoreId=@p1 and a.TransDate >=@p2 and a.TransDate<=@p3
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
            where a.StoreId=@p1 and a.TransDate >=@p2 and a.TransDate<=@p3 and c.Type='Combo' 
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
			where a.StoreId=@p1 and a.TransDate >=@p2 and a.TransDate<=@p3 
            
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
            where a.StoreId=@p1 and a.TransDate >=@p2 and a.TransDate<=@p3 
            group by d.Name,c.Name,a.TransDate
            
                ";

            return _context.Database.SqlQuery<BranchReportSaleViewModel>(sql, parameters.ToArray()).ToList();
        }
        public List<SalesReportViewModel> GenerateSalesData(int storeId, DateTime dateFrom, DateTime dateTo)
        {
            var parameters = new List<SqlParameter> { new SqlParameter("@p1", storeId), new SqlParameter("@p2", dateFrom), new SqlParameter("@p3", dateTo) };
            var sql = @"select a.TransCode as InvoiceNumber,SUM(b.Quantity) as Qty,SUM(b.UnitPrice*b.Quantity)as Amount,a.TransDate as Date 
            ,a.Discount as Discount,a.Tax as Tax
			from PosCloud.TransMaster as a 
            inner join PosCloud.TransDetails as b on a.id=b.TransMasterId
			inner join PosCloud.Stores as d on a.StoreId=d.Id
            where a.StoreId=@p1 and a.TransDate >=@p2 and a.TransDate<=@p3 
            group by a.TransCode,a.TransCode,a.TransDate,a.Discount,a.Tax
            
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
            where a.StoreId=@p1 and a.TransDate >=@p2 and a.TransDate<=@p3
            group by  c.Name,b.Discount,CONVERT(time(0),(CONVERT(VARCHAR(2), a.TransDate, 108)+':00'))
            
                ";
            var data = _context.Database.SqlQuery<ProductTimelySaleViewModel>(sql, parameters.ToArray()).ToList();
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
            where a.StoreId=@p1 and a.TransDate >=@p2 and a.TransDate<=@p3 
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
            where a.StoreId=@p1 and a.TransDate >=@p2 and a.TransDate<=@p3 and f.Type = 'C'
             group by f.Name,a.TransCode,a.TransDate,a.Discount,a.Tax
                ";
            var data = _context.Database.SqlQuery<CustomerSaleReportViewModel>(sql, parameters.ToArray()).ToList();
            return data;
        }
    }
}
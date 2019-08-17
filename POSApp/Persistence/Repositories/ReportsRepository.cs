using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using AutoMapper;
using Microsoft.AspNet.Identity;
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
        public List<SubReportViewModel> GenerateSubReportData(int storeId,string details,string reportName)
        {
            var parameters = new List<SqlParameter>();
            var sql = "";
            if (storeId != 0)
            {

             parameters = new List<SqlParameter> { new SqlParameter("@p1", storeId) };
             sql = @"select a.Name as CompanyName,a.Contact as PhoneNumber,a.Address as Address
			from PosCloud.Clients as a 
            inner join PosCloud.Stores as d on a.Id=d.ClientId
            where d.Id = @p1 and a.IsDisabled = 0
            group by a.Name,a.Contact,a.Address
                ";
            }
            else
            {
                 parameters = new List<SqlParameter> { new SqlParameter("@p1", HttpContext.Current.User.Identity.GetUserId()) };
                 sql = @"select c.Name as CompanyName,c.Contact as PhoneNumber,c.Address as Address
			from dbo.AspNetUsers as us
            inner join PosCloud.Stores as d on us.StoreId=d.Id
			inner join PosCloud.Clients as c on d.ClientId=c.Id
            where us.Id = @p1 and c.IsDisabled = 0
            group by c.Name,c.Contact,c.Address
                ";
            }
            var data = _context.Database.SqlQuery<SubReportViewModel>(sql,parameters.ToArray()).ToList();
            foreach (var subReportViewModel in data)
            {
                subReportViewModel.ReportName = reportName;
                subReportViewModel.Details = details;
            }
            return data;
        }

        public List<BatchWiseExpReportViewModel> BatchWiseExpiryData(List<int> storeIds, DateTime dateFrom, DateTime dateTo)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-UK");
            IFormatProvider UK = new CultureInfo("en-UK").DateTimeFormat;
            dateFrom = Convert.ToDateTime(dateFrom, UK);
            dateTo = Convert.ToDateTime(dateTo, UK);

            var parameters = new List<SqlParameter> { new SqlParameter("@p3", dateFrom),
                new SqlParameter("@p4", dateTo),
                new SqlParameter("@d", string.Join(",",storeIds))
            };
            var sql = @" exec BatchWiseExpiry @d,@p3,@p4 ";
            return _context.Database.SqlQuery<BatchWiseExpReportViewModel>(sql, parameters.ToArray()).ToList();
        }
        public List<ProductSalesReportViewModel> GenerateProductSalesData(List<int> categoryIds, List<string> sizes,  List<int> storeIds, DateTime dateFrom, DateTime dateTo)
        {

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-UK");
            IFormatProvider UK = new CultureInfo("en-UK").DateTimeFormat;
            dateFrom = Convert.ToDateTime(dateFrom, UK);
            dateTo = Convert.ToDateTime(dateTo, UK);
            var parameters = new List<SqlParameter> { new SqlParameter("@p3", dateFrom),
                new SqlParameter("@p4", dateTo)
                ,new SqlParameter("@d", string.Join(",",storeIds))
                ,new SqlParameter("@e", string.Join(",",categoryIds))
                ,new SqlParameter("@f", string.Join(",",sizes))
            };
            
            var sql = @" exec ProductSale @d,@e,@f,@p3,@p4
                ";
            var data= _context.Database.SqlQuery<ProductSalesReportViewModel>(sql, parameters.ToArray()).ToList();
            return data;
        }
        public List<AgentIncomeReportViewModel> GenerateEmployeeIncomeData(List<int> storeIds, List<int> designationIds)
        {
            var parameters = new List<SqlParameter> {new SqlParameter("@d", string.Join(",",storeIds))
                ,new SqlParameter("@e", string.Join(",",designationIds))};

           

            var sql = @" exec EmployeeIncome @d,@e
                ";
            var data = _context.Database.SqlQuery<AgentIncomeReportViewModel>(sql, parameters.ToArray()).ToList();
            return data;
        }
        public decimal GetProductSalesDiscount(List<int> storeIds, DateTime dateFrom, DateTime dateTo)
        {
            var parameters = new List<SqlParameter> { new SqlParameter("@p3", dateFrom),
                new SqlParameter("@p4", dateTo),
            };

            parameters.AddRange(storeIds.Select((id, i) => new SqlParameter("d" + i, id)));

            var sql = @"select Sum(Discount)
                from PosCloud.TransMaster 
           where StoreId in (" + string.Join(",", parameters.Where(a => a.ParameterName.Contains("d")).Select(p => "@" + p.ParameterName)) + @") and TransDate >=@p3 and TransDate<=@p4
                ";
            var data = _context.Database.SqlQuery<decimal>(sql, parameters.ToArray()).Sum();
            return data;
        }


        public List<CategoryReportViewModel> GenerateCategoriesSalesData(List<int> storeIds, DateTime dateFrom, DateTime dateTo)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-UK");
            IFormatProvider UK = new CultureInfo("en-UK").DateTimeFormat;
            dateFrom = Convert.ToDateTime(dateFrom, UK);
            dateTo = Convert.ToDateTime(dateTo, UK);
            var parameters = new List<SqlParameter> { new SqlParameter("@p3", dateFrom),
                new SqlParameter("@p4", dateTo),
            };

            parameters.AddRange(storeIds.Select((id, i) => new SqlParameter("d" + i, id)));
            var sql = @" exec @d,@p3,@p4
                ";

            return _context.Database.SqlQuery<CategoryReportViewModel>(sql, parameters.ToArray()).ToList();
        }
        public List<ProductSizeSaleViewModel> GenerateProductSizeWiseSalesData(List<int> storeIds, DateTime dateFrom, DateTime dateTo)
        {

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-UK");
            IFormatProvider UK = new CultureInfo("en-UK").DateTimeFormat;
            dateFrom = Convert.ToDateTime(dateFrom, UK);
            dateTo = Convert.ToDateTime(dateTo, UK);
            var parameters = new List<SqlParameter> { new SqlParameter("@p3", dateFrom),
                new SqlParameter("@p4", dateTo),
            };

            parameters.AddRange(storeIds.Select((id, i) => new SqlParameter("d" + i, id)));

            var sql = @"select c.Name as ProductName,c.Size as Size,SUM(b.Quantity) as Qty,SUM(b.UnitPrice*b.Quantity)as Amount,a.TransDate as Date from PosCloud.TransMaster as a 
            inner join PosCloud.TransDetails as b on a.id=b.TransMasterId
            inner join PosCloud.Products as c on b.ProductCode=c.ProductCode 
            where a.StoreId in (" + string.Join(",", parameters.Where(a => a.ParameterName.Contains("d")).Select(p => "@" + p.ParameterName)) + @") and a.TransDate >=@p3 and a.TransDate<=@p4 and c.InventoryItem ='0' and a.Type= 'INV' and ((a.TransStatus = 'Paid' or a.TransStatus = 'Complete') or a.TransStatus = 'Complete')
            group by c.Name,c.Size,a.TransDate
                ";

            return _context.Database.SqlQuery<ProductSizeSaleViewModel>(sql, parameters.ToArray()).ToList();
        }
        public List<ProductSizeOrderTypeReportViewModel> GenerateProductSizeOrderTypeSalesData(List<int> storeIds, DateTime dateFrom, DateTime dateTo)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-UK");
            IFormatProvider UK = new CultureInfo("en-UK").DateTimeFormat;
            dateFrom = Convert.ToDateTime(dateFrom, UK);
            dateTo = Convert.ToDateTime(dateTo, UK);
            var parameters = new List<SqlParameter> { new SqlParameter("@p3", dateFrom),
                new SqlParameter("@p4", dateTo),
            };

            parameters.AddRange(storeIds.Select((id, i) => new SqlParameter("d" + i, id)));


            var sql = @"select c.Name as ProductName,a.DeliveryType as OrderType,c.Size as Size,SUM(b.Quantity) as Qty,SUM(b.UnitPrice*b.Quantity)as Amount,a.TransDate as Date from PosCloud.TransMaster as a 
            inner join PosCloud.TransDetails as b on a.id=b.TransMasterId
			
            inner join PosCloud.Products as c on b.ProductCode=c.ProductCode 
            where a.StoreId in (" + string.Join(",", parameters.Where(a => a.ParameterName.Contains("d")).Select(p => "@" + p.ParameterName)) + @") and a.TransDate >=@p3 and a.TransDate<=@p4 and c.InventoryItem ='0' and a.Type= 'INV' and ((a.TransStatus = 'Paid' or a.TransStatus = 'Complete') or a.TransStatus = 'Complete')
            group by c.Name,c.Size,a.DeliveryType,a.TransDate
                ";

            return _context.Database.SqlQuery<ProductSizeOrderTypeReportViewModel>(sql, parameters.ToArray()).ToList();
        }
        public List<OrderTypeSaleReportViewModel> GenerateProductOrderTypeSalesData(List<int> storeIds, DateTime dateFrom, DateTime dateTo)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-UK");
            IFormatProvider UK = new CultureInfo("en-UK").DateTimeFormat;
            dateFrom = Convert.ToDateTime(dateFrom, UK);
            dateTo = Convert.ToDateTime(dateTo, UK);
            var parameters = new List<SqlParameter> { new SqlParameter("@p3", dateFrom),
                new SqlParameter("@p4", dateTo),
            };

            parameters.AddRange(storeIds.Select((id, i) => new SqlParameter("d" + i, id)));


            var sql = @"select a.DeliveryType as OrderType,a.TransCode as InvoiceNumber,SUM(b.Quantity) as Qty,SUM(b.UnitPrice*b.Quantity)as Amount,a.Discount,a.Tax,a.TransDate as Date from PosCloud.TransMaster as a 
            inner join PosCloud.TransDetails as b on a.id=b.TransMasterId
            inner join PosCloud.Products as c on b.ProductCode=c.ProductCode 
            where a.StoreId    in (" + string.Join(",", parameters.Where(a => a.ParameterName.Contains("d")).Select(p => "@" + p.ParameterName)) + @") and a.TransDate >=@p3 and a.TransDate<=@p4 and c.InventoryItem = '0' and a.Type= 'INV' and ((a.TransStatus = 'Paid' or a.TransStatus = 'Complete') or a.TransStatus = 'Complete')
            group by a.TransCode,a.DeliveryType,a.TransDate,a.Discount,a.Tax
                ";

            return _context.Database.SqlQuery<OrderTypeSaleReportViewModel>(sql, parameters.ToArray()).ToList();
        }
        public List<ComboReportSaleViewModel> GenerateComboSalesData(List<int> storeIds, DateTime dateFrom, DateTime dateTo)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-UK");
            IFormatProvider UK = new CultureInfo("en-UK").DateTimeFormat;
            dateFrom = Convert.ToDateTime(dateFrom, UK);
            dateTo = Convert.ToDateTime(dateTo, UK);
            var parameters = new List<SqlParameter> { new SqlParameter("@p3", dateFrom),
                new SqlParameter("@p4", dateTo),
                new SqlParameter("@d", string.Join(",",storeIds))
            };

            

            var sql = @"exec ComboSale @d,@p3,@p4
                ";

            return _context.Database.SqlQuery<ComboReportSaleViewModel>(sql, parameters.ToArray()).ToList();
        }
        public List<ModifierReportViewModel> GenerateModifierSalesData(List<int> storeIds, DateTime dateFrom, DateTime dateTo)
        {

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-UK");
            IFormatProvider UK = new CultureInfo("en-UK").DateTimeFormat;
            dateFrom = Convert.ToDateTime(dateFrom, UK);
            dateTo = Convert.ToDateTime(dateTo, UK);
            var parameters = new List<SqlParameter> { new SqlParameter("@p3", dateFrom),
                new SqlParameter("@p4", dateTo),
                new SqlParameter("@d",string.Join(",",storeIds))
                
            };

            


            var sql = @"exec ModifierSale @d,@p3,@p4
                ";

            return _context.Database.SqlQuery<ModifierReportViewModel>(sql, parameters.ToArray()).ToList();
        }
        public List<BranchReportSaleViewModel> GenerateBranchSalesData(List<int> storeIds, DateTime dateFrom, DateTime dateTo)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-UK");
            IFormatProvider UK = new CultureInfo("en-UK").DateTimeFormat;
            dateFrom = Convert.ToDateTime(dateFrom, UK);
            dateTo = Convert.ToDateTime(dateTo, UK);
            var parameters = new List<SqlParameter> { new SqlParameter("@p3", dateFrom),
                new SqlParameter("@p4", dateTo),
            };

            parameters.AddRange(storeIds.Select((id, i) => new SqlParameter("d" + i, id)));



            var sql = @"select d.Name as BranchName,c.Name as ProductName,SUM(b.Quantity) as Qty,SUM(b.UnitPrice*b.Quantity)as Amount,a.TransDate as Date 
,b.UnitPrice,c.CostPrice,b.Tax,b.Discount 
			from PosCloud.TransMaster as a 
            inner join PosCloud.TransDetails as b on a.id=b.TransMasterId
            inner join PosCloud.Products as c on b.ProductCode=c.ProductCode
			inner join PosCloud.Stores as d on a.StoreId=d.Id
            where a.StoreId in (" + string.Join(",", parameters.Where(a => a.ParameterName.Contains("d")).Select(p => "@" + p.ParameterName)) + @") and  CONVERT(VARCHAR(20), a.TransDate, 101)>=@p3 and  CONVERT(VARCHAR(20), a.TransDate, 101)<=@p4 and c.InventoryItem = '0' and a.Type= 'INV' and ((a.TransStatus = 'Paid' or a.TransStatus = 'Complete') or a.TransStatus = 'Complete')
              group by d.Name,c.Name,a.TransDate,b.UnitPrice,c.CostPrice,b.Tax,b.Discount
            
                ";

            return _context.Database.SqlQuery<BranchReportSaleViewModel>(sql, parameters.ToArray()).ToList();
        }
        public List<SalesReportViewModel> GenerateSalesData(List<int> storeIds, List<string> methods, List<string> orderTypes, List<int> customers ,DateTime dateFrom, DateTime dateTo)
        {


            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-UK");
            IFormatProvider UK = new CultureInfo("en-UK").DateTimeFormat;
            dateFrom = Convert.ToDateTime(dateFrom, UK);
            dateTo = Convert.ToDateTime(dateTo, UK);
            var parameters = new List<SqlParameter> { new SqlParameter("@p3", dateFrom),
                new SqlParameter("@p4", dateTo),
                new SqlParameter("@d", string.Join(",",storeIds)),
                new SqlParameter("@e", string.Join(",",methods)),
                new SqlParameter("@f", string.Join(",",orderTypes)),
                new SqlParameter("@g", string.Join(",",customers))
            };

         
            var sql = @"
             exec Sales @d,@e,@f,@g,@p3,@p4


                ";
            var query = _context.Database.SqlQuery<SalesReportViewModel>(sql, parameters.ToArray()).ToList();
            return query;
        }
        public List<PaymentMethodReportViewModel> GeneratePaymentMethodWiseSalesData(List<int> storeIds, DateTime dateFrom, DateTime dateTo)
        {

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-UK");
            IFormatProvider UK = new CultureInfo("en-UK").DateTimeFormat;
            dateFrom = Convert.ToDateTime(dateFrom, UK);
            dateTo = Convert.ToDateTime(dateTo, UK);
            var parameters = new List<SqlParameter> { new SqlParameter("@p3", dateFrom),
                new SqlParameter("@p4", dateTo),
            };

            parameters.AddRange(storeIds.Select((id, i) => new SqlParameter("d" + i, id)));
            var sql = @"select d.Method as PaymentMethod,a.TransCode as InvoiceNumber,SUM(b.Quantity) as Qty,SUM(b.UnitPrice*b.Quantity)as Amount,a.Discount,a.Tax,a.TransDate as Date 
            
			from PosCloud.TransMaster as a 
            inner join PosCloud.TransDetails as b on a.id=b.TransMasterId
			inner join PosCloud.TransMasterPaymentMethods as d on a.Id=d.TransMasterId
			inner join PosCloud.Products as c on b.ProductCode=c.ProductCode
            where a.StoreId  in (" + string.Join(",", parameters.Where(a => a.ParameterName.Contains("d")).Select(p => "@" + p.ParameterName)) + @") and a.TransDate >=@p3 and a.TransDate<=@p4
            group by  d.Method,a.Discount,a.Tax,a.TransCode,a.TransDate
            
                ";

            return _context.Database.SqlQuery<PaymentMethodReportViewModel>(sql, parameters.ToArray()).ToList();
        }
        public List<DineTableReportViewModel> GenerateTableWiseSalesData(List<int> storeIds, List<int> tableIds,DateTime dateFrom, DateTime dateTo)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-UK");
            IFormatProvider UK = new CultureInfo("en-UK").DateTimeFormat;
            dateFrom = Convert.ToDateTime(dateFrom, UK);
            dateTo = Convert.ToDateTime(dateTo, UK);
            var parameters = new List<SqlParameter> { new SqlParameter("@p3", dateFrom),
                new SqlParameter("@p4", dateTo),
                new SqlParameter("@d",string.Join(",",storeIds)),
                new SqlParameter("@e",string.Join(",",tableIds))
            };
            

            var sql = @" exec TableSale @d,@e,@p3,@p4 ";

            return _context.Database.SqlQuery<DineTableReportViewModel>(sql, parameters.ToArray()).ToList();
        }
        public List<ProductTimelySaleViewModel> GenerateProductTimeWiseSalesData(List<int> storeIds, DateTime dateFrom, DateTime dateTo)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-UK");
            IFormatProvider UK = new CultureInfo("en-UK").DateTimeFormat;
            dateFrom = Convert.ToDateTime(dateFrom, UK);
            dateTo = Convert.ToDateTime(dateTo, UK);
            var parameters = new List<SqlParameter> { new SqlParameter("@p3", dateFrom),
                new SqlParameter("@p4", dateTo),
            };

            parameters.AddRange(storeIds.Select((id, i) => new SqlParameter("d" + i, id)));



            var sql = @"select CONVERT(time(0),(CONVERT(VARCHAR(2), a.TransDate, 108))+':00') as Time,c.Name as ProductName,SUM(b.Quantity) as Quantity,SUM(b.UnitPrice*b.Quantity)as UnitPrice,b.Discount as Discount from PosCloud.TransMaster as a 
            inner join PosCloud.TransDetails as b on a.id=b.TransMasterId
            inner join PosCloud.Products as c on b.ProductCode=c.ProductCode  
            where a.StoreId in (" + string.Join(",", parameters.Where(a => a.ParameterName.Contains("d")).Select(p => "@" + p.ParameterName)) + @") and a.TransDate >=@p3 and a.TransDate<=@p4 and c.InventoryItem ='0'  and a.Type= 'INV' and ((a.TransStatus = 'Paid' or a.TransStatus = 'Complete') or a.TransStatus = 'Complete')
            group by  c.Name,b.Discount,CONVERT(time(0),(CONVERT(VARCHAR(2), a.TransDate, 108)+':00'))
            
                ";
            var data = _context.Database.SqlQuery<ProductTimelySaleViewModel>(sql, parameters.ToArray()).ToList();
            return data;
        }
        public List<PaymentMethodTimelySaleViewModel> GeneratePaymentMethodTimeWiseSalesData(List<int> storeIds, DateTime dateFrom, DateTime dateTo)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-UK");
            IFormatProvider UK = new CultureInfo("en-UK").DateTimeFormat;
            dateFrom = Convert.ToDateTime(dateFrom, UK);
            dateTo = Convert.ToDateTime(dateTo, UK);
            var parameters = new List<SqlParameter> { new SqlParameter("@p3", dateFrom),
                new SqlParameter("@p4", dateTo),
            };

            parameters.AddRange(storeIds.Select((id, i) => new SqlParameter("d" + i, id)));

            var sql = @"select CONVERT(time(0),(CONVERT(VARCHAR(2), a.TransDate, 108))+':00') as Time,d.Method as PaymentMethod,a.TransCode as InvoiceNumber,SUM(b.Quantity) as Qty,SUM(b.UnitPrice*b.Quantity)as Amount,a.Discount,a.Tax 
            
			from PosCloud.TransMaster as a 
            inner join PosCloud.TransDetails as b on a.id=b.TransMasterId
			inner join PosCloud.TransMasterPaymentMethods as d on a.Id=d.TransMasterId
			inner join PosCloud.Products as c on b.ProductCode=c.ProductCode
             where a.StoreId in (" + string.Join(",", parameters.Where(a => a.ParameterName.Contains("d")).Select(p => "@" + p.ParameterName)) + @") and a.TransDate >=@p3 and a.TransDate<=@p4 and a.Type= 'INV' and ((a.TransStatus = 'Paid' or a.TransStatus = 'Complete') or a.TransStatus = 'Complete')
            group by d.Method,a.Discount,a.Tax,a.TransCode,CONVERT(time(0),(CONVERT(VARCHAR(2), a.TransDate, 108)+':00')) 
            
            
            
                ";
            var data = _context.Database.SqlQuery<PaymentMethodTimelySaleViewModel>(sql, parameters.ToArray()).ToList();
            return data;
        }
        public List<BranchTimelyReportViewModel> GenerateTimelyBranchSalesData(List<int> storeIds, DateTime dateFrom, DateTime dateTo)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-UK");
            IFormatProvider UK = new CultureInfo("en-UK").DateTimeFormat;
            dateFrom = Convert.ToDateTime(dateFrom, UK);
            dateTo = Convert.ToDateTime(dateTo, UK);
            var parameters = new List<SqlParameter> { new SqlParameter("@p3", dateFrom),
                new SqlParameter("@p4", dateTo),
            };

            parameters.AddRange(storeIds.Select((id, i) => new SqlParameter("d" + i, id)));



            var sql = @"select d.Name as BranchName,c.Name as ProductName,SUM(b.Quantity) as Qty,SUM(b.UnitPrice*b.Quantity)as Amount,CONVERT(time(0),(CONVERT(VARCHAR(2), a.TransDate, 108))+':00') as Time
			from PosCloud.TransMaster as a 
            inner join PosCloud.TransDetails as b on a.id=b.TransMasterId
            inner join PosCloud.Products as c on b.ProductCode=c.ProductCode
			inner join PosCloud.Stores as d on a.StoreId=d.Id
            where a.StoreId  
in (" + string.Join(",", parameters.Where(a => a.ParameterName.Contains("d")).Select(p => "@" + p.ParameterName)) + @") and a.TransDate >=@p3 and a.TransDate<=@p4 and c.InventoryItem ='0' and a.Type= 'INV' and ((a.TransStatus = 'Paid' or a.TransStatus = 'Complete') or a.TransStatus = 'Complete')
            group by d.Name,c.Name,CONVERT(time(0),(CONVERT(VARCHAR(2), a.TransDate, 108)+':00'))
                ";
            var data = _context.Database.SqlQuery<BranchTimelyReportViewModel>(sql, parameters.ToArray()).ToList();
            return data;
        }
        public List<CustomerSaleReportViewModel> GenerateCustomerWiseSalesData(List<int> storeIds, DateTime dateFrom, DateTime dateTo)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-UK");
            IFormatProvider UK = new CultureInfo("en-UK").DateTimeFormat;
            dateFrom = Convert.ToDateTime(dateFrom, UK);
            dateTo = Convert.ToDateTime(dateTo, UK);
            var parameters = new List<SqlParameter> { new SqlParameter("@p3", dateFrom),
                new SqlParameter("@p4", dateTo),
            };

            parameters.AddRange(storeIds.Select((id, i) => new SqlParameter("d" + i, id)));

            var sql = @"select f.Name as CustomerName,a.TransCode as InvoiceNumber,SUM(b.Quantity) as Qty,SUM(b.UnitPrice*b.Quantity)as Amount,a.TransDate as Date 
            ,a.Discount as Discount,a.Tax as Tax
			from PosCloud.TransMaster as a 
            inner join PosCloud.TransDetails as b on a.id=b.TransMasterId
            inner join PosCloud.BusinessPartners as f on a.BusinessPartnerId = f.Id
            where a.StoreId  in (" + string.Join(",", parameters.Where(a => a.ParameterName.Contains("d")).Select(p => "@" + p.ParameterName)) + @") and a.TransDate >=@p3 and a.TransDate<=@p4 and f.Type = 'C' and a.Type= 'INV' and ((a.TransStatus = 'Paid' or a.TransStatus = 'Complete') or a.TransStatus = 'Complete')
             group by f.Name,a.TransCode,a.TransDate,a.Discount,a.Tax
                ";
            var data = _context.Database.SqlQuery<CustomerSaleReportViewModel>(sql, parameters.ToArray()).ToList();
            return data;
        }
        public List<ModifierReportViewModel> GenerateProductModifierSalesData(List<int> storeIds, DateTime dateFrom, DateTime dateTo)
        {

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-UK");
            IFormatProvider UK = new CultureInfo("en-UK").DateTimeFormat;
            dateFrom = Convert.ToDateTime(dateFrom, UK);
            dateTo = Convert.ToDateTime(dateTo, UK);
            var parameters = new List<SqlParameter> { new SqlParameter("@p3", dateFrom),
                new SqlParameter("@p4", dateTo),
            };

            parameters.AddRange(storeIds.Select((id, i) => new SqlParameter("d" + i, id)));

            var sql = @"select g.Name as ProductName,e.Name as ModifierName,f.Name as ModifierOption,a.TransCode as InvoiceNumber,SUM(b.Quantity) as Qty,SUM(b.UnitPrice*b.Quantity)as Amount,a.TransDate as Date 
            ,a.Discount as Discount,a.Tax as Tax
			from PosCloud.TransMaster as a 
            inner join PosCloud.TransDetails as b on a.id=b.TransMasterId
            inner join PosCloud.ModifierTransDetails as c on b.Id = c.TransDetailId
			inner join PosCloud.ModifierOptions as f on c.ModifierOptionId= f.Id
			inner join PosCloud.Modifier as e on f.ModifierId = e.Id
			inner join PosCloud.Products as g on g.ProductCode=b.ProductCode
			where a.StoreId in (" + string.Join(",", parameters.Where(a => a.ParameterName.Contains("d")).Select(p => "@" + p.ParameterName)) + @") and a.TransDate >=@p3 and a.TransDate<=@p4 and a.Type= 'INV' and ((a.TransStatus = 'Paid' or a.TransStatus = 'Complete') or a.TransStatus = 'Complete')
            group by e.Name,f.Name,g.Name,a.TransCode,a.TransDate,a.Discount,a.Tax
                ";

            return _context.Database.SqlQuery<ModifierReportViewModel>(sql, parameters.ToArray()).ToList();
        }

        public List<RecipeReportViewModel> GenerateProductRecipeData(List<int> storeIds, List<string> productCodes)
        {
            var parameters = new List<SqlParameter> { };
            parameters.AddRange(storeIds.Select((id, i) => new SqlParameter("d" + i, id)));
            parameters.AddRange(productCodes.Select((id, i) => new SqlParameter("e" + i, id)));
            var sql = @"SELECT t1.Name as ProductName,t.Id, t.IngredientCode, t.Quantity, t.ExpiryDate,t1.Name AS ProductName, t2.Name AS IngredientName,ISNull(t.Calories,0) as Calories, t2.IngredientUnit as Unit
                         FROM  PosCloud.Recipes AS t INNER JOIN
                         PosCloud.Products AS t1 ON t1.ProductCode = t.ProductCode AND t1.StoreId = t.StoreId  INNER JOIN
                         PosCloud.Products AS t2 ON t2.ProductCode = t.IngredientCode AND t2.StoreId = t.StoreId 
                      
			             where t.StoreId in (" + string.Join(",", parameters.Where(a => a.ParameterName.Contains("d")).Select(p => "@" + p.ParameterName)) + @")
and t.ProductCode in (" + string.Join(",", parameters.Where(a => a.ParameterName.Contains("e")).Select(p => "@" + p.ParameterName)) + @")
";
            var query = _context.Database.SqlQuery<RecipeReportViewModel>(sql, parameters.ToArray()).ToList();
            return query;
        }


        public List<StockReportViewModel> GenerateStockData(List<int> storeIds, List<string> productCodes)
        {
            var parameters = new List<SqlParameter> { };
            parameters.AddRange(storeIds.Select((id, i) => new SqlParameter("d" + i, id)));
            parameters.AddRange(productCodes.Select((id, i) => new SqlParameter("e" + i, id)));
            var sql = @"Select a.ProductCode,a.Name,a.PurchaseUnit as Unit,
ISNULL(
						 SUM(CASE WHEN c.Type in ('OPS') THEN d.Quantity ELSE 0 END),0) as OpeningStock
                          ,ISNULL(
                           SUM(CASE WHEN c.Type in ('STI') THEN d.Quantity ELSE 0 END),0) as StockIn
                          ,ISNULL(
                           SUM(CASE WHEN c.Type in ('EXP') THEN d.Quantity ELSE 0 END),0) as Expired
                          ,ISNULL(
                           SUM(CASE WHEN c.Type in ('DMG') THEN d.Quantity ELSE 0 END),0) as Damaged
                          ,ISNULL(
                           SUM(CASE WHEN c.Type in ('WST') THEN d.Quantity ELSE 0 END),0) as Wasted
                          ,ISNULL(
                           SUM(CASE WHEN c.Type in ('OTO','TRS') THEN d.Quantity ELSE 0 END),0) as Transferred
                          ,ISNULL(
                           SUM(CASE WHEN c.Type in ('OTI') THEN d.Quantity ELSE 0 END),0) as OtherIn
                          ,ISNULL(
                           SUM(CASE WHEN c.Type in ('REF') THEN d.Quantity ELSE 0 END),0) as Refunded
                          ,
						   ISNULL(
						 SUM(CASE WHEN c.Type in ('MIF') THEN d.Quantity/(a.PtoSFactor*a.StoIFactor) ELSE 0 END),0) as Utilized
						 from PosCloud.Products as a inner join 
						 PosCloud.TransDetails as d on a.ProductCode = d.ProductCode and a.StoreId=d.StoreId
						  inner join PosCloud.TransMaster as c
						 on c.Id=d.TransMasterId AND c.StoreId=d.StoreId
						  where a.StoreId in (" + string.Join(",", parameters.Where(a => a.ParameterName.Contains("d")).Select(p => "@" + p.ParameterName)) + @") AND a.ProductCode in (" + string.Join(",", parameters.Where(a => a.ParameterName.Contains("e")).Select(p => "@" + p.ParameterName)) + @") AND a.InventoryItem=1
						 GROUP BY a.ProductCode,a.Name,a.PurchaseUnit

						
			            ";

           
                var query=_context.Database.SqlQuery<StockReportViewModel>(sql, parameters.ToArray()).ToList();
                return query;
        }

        public List<StockReportViewModel> GenerateProductsStockData(int storeId)
        {
           
                

            var sql = @"Select a.ProductCode,a.ReOrderLevel,a.Name,a.PurchaseUnit as Unit,
ISNULL(
						 SUM(CASE WHEN c.Type in ('OPS') THEN d.Quantity ELSE 0 END),0) as OpeningStock
                          ,ISNULL(
                           SUM(CASE WHEN c.Type in ('STI') THEN d.Quantity ELSE 0 END),0) as StockIn
                          ,ISNULL(
                           SUM(CASE WHEN c.Type in ('EXP') THEN d.Quantity ELSE 0 END),0) as Expired
                          ,ISNULL(
                           SUM(CASE WHEN c.Type in ('DMG') THEN d.Quantity ELSE 0 END),0) as Damaged
                          ,ISNULL(
                           SUM(CASE WHEN c.Type in ('WST') THEN d.Quantity ELSE 0 END),0) as Wasted
                          ,ISNULL(
                           SUM(CASE WHEN c.Type in ('OTO','TRS') THEN d.Quantity ELSE 0 END),0) as Transferred
                          ,ISNULL(
                           SUM(CASE WHEN c.Type in ('OTI') THEN d.Quantity ELSE 0 END),0) as OtherIn
                          ,ISNULL(
                           SUM(CASE WHEN c.Type in ('REF') THEN d.Quantity ELSE 0 END),0) as Refunded
                          ,
						   ISNULL(
						 SUM(CASE WHEN c.Type in ('MIF') THEN d.Quantity/(a.PtoSFactor*a.StoIFactor) ELSE 0 END),0) as Utilized
						 from PosCloud.Products as a inner join 
						 PosCloud.TransDetails as d on a.ProductCode = d.ProductCode and a.StoreId=d.StoreId
						  inner join PosCloud.TransMaster as c
						 on c.Id=d.TransMasterId AND c.StoreId=d.StoreId
						  where a.InventoryItem=1
						 GROUP BY a.ProductCode,a.Name,a.PurchaseUnit,a.ReOrderLevel

						
			            ";


            var query = _context.Database.SqlQuery<StockReportViewModel>(sql).ToList();
            return query;
        }

        public List<WarehouseStockReportViewModel> GenerateWarehouseStock(List<int> warehouseIds, List<string> productCodes)
        {

            var parameters = new List<SqlParameter> { };
            parameters.AddRange(warehouseIds.Select((id, i) => new SqlParameter("d" + i, id)));
            parameters.AddRange(productCodes.Select((id, i) => new SqlParameter("E" + i, id)));
            var sql = @" Select a.ProductCode,a.Name,a.PurchaseUnit, ISNULL(
      (select SUM(d.Quantity) from
      PosCloud.TransMaster as c inner join PosCloud.TransDetails as d
      on c.Id=d.TransMasterId AND c.StoreId=d.StoreId
       inner join PosCloud.Warehouses as w on c.WarehouseId = w.Id
      
      where d.ProductCode=a.ProductCode AND d.StoreId=a.StoreId AND w.Id = ware.Id AND c.Type in ('STK') ),0) as StockTaking,
        
       ISNULL(
      (select SUM(d.Quantity) from
      PosCloud.TransMaster as c inner join PosCloud.TransDetails as d
      on c.Id=d.TransMasterId AND c.StoreId=d.StoreId
       inner join PosCloud.Warehouses as w on c.WarehouseId = w.Id
      
      where d.ProductCode=a.ProductCode AND d.StoreId=a.StoreId AND w.Id = ware.Id AND c.Type in ('PRI') ),0) as Purchased,
      	
      		  ISNULL(
      (select SUM(d.Quantity) from
      PosCloud.TransMaster as c inner join PosCloud.TransDetails as d
      on c.Id=d.TransMasterId AND c.StoreId=d.StoreId
      inner join PosCloud.Warehouses as w on c.WarehouseId = w.Id
      
      where d.ProductCode=a.ProductCode AND d.StoreId=a.StoreId AND w.Id = ware.Id AND c.Type in ('STI') ),0) as StockIn
      
      from PosCloud.Products as a inner join 
	  PosCloud.Stores as s on a.StoreId=s.Id inner join
	  PosCloud.Clients as cl on s.ClientId=cl.Id inner join
	  PosCloud.Warehouses as ware on cl.Id=ware.ClientId
	  where ware.Id in (" + string.Join(",", parameters.Where(a => a.ParameterName.Contains("d")).Select(p => "@" + p.ParameterName)) + @") and
      a.ProductCode in (" + string.Join(",", parameters.Where(a => a.ParameterName.Contains("E")).Select(p => "@" + p.ParameterName)) + @") 
          
                ";

            return _context.Database.SqlQuery<WarehouseStockReportViewModel>(sql, parameters.ToArray()).ToList();
        }
        public List<PurchaseReportViewModel> GeneratePurchaseOrderData(List<int> storeIds, DateTime dateFrom, DateTime dateTo, List<int> supplierId)
        {
            var parameters = new List<SqlParameter> { new SqlParameter("@p2", dateFrom), new SqlParameter("@p3", dateTo) };
            parameters.AddRange(storeIds.Select((id, i) => new SqlParameter("d" + i, id)));
            parameters.AddRange(supplierId.Select((id, i) => new SqlParameter("E" + i, id)));
            var sql = @" select d.Name as BranchName,c.Name as SupplierName,p.Name as ProductName,a.TransCode as InvoiceNumber,SUM(b.Quantity) as Qty,a.TotalPrice as Amount 
            
			from PosCloud.TransMaster as a 
			inner join PosCloud.TransDetails as b on a.id=b.TransMasterId
			inner join POsCloud.Products as p on b.ProductCode = p.ProductCode
           inner join PosCloud.BusinessPartners as c on a.BusinessPartnerId  = c.Id
			inner join PosCloud.Stores as d on a.StoreId=d.Id
            where a.Type = 'PRO' and a.StoreId in (" + string.Join(",", parameters.Where(a => a.ParameterName.Contains("d")).Select(p => "@" + p.ParameterName)) + @")  
            and a.TransDate >=@p2 and a.TransDate<=@p3
            and a.BusinessPartnerId in (" + string.Join(",", parameters.Where(a => a.ParameterName.Contains("E")).Select(p => "@" + p.ParameterName)) + @") 
            group by d.Name,a.TransCode,a.TotalPrice,p.Name,c.Name

            
                ";
            var query = _context.Database.SqlQuery<PurchaseReportViewModel>(sql, parameters.ToArray()).ToList();
            return query;
        }
        public List<PurchaseReportViewModel> GeneratePurchasesPerSupplierData(List<int> storeIds, int supplierId)
        {
            var parameters = new List<SqlParameter> { new SqlParameter("@p3", supplierId),
               
            };

            parameters.AddRange(storeIds.Select((id, i) => new SqlParameter("d" + i, id)));


            var sql = @" select d.Name as BranchName,a.TransCode as InvoiceNumber,SUM(b.Quantity) as Qty,p.PurchaseUnit as Unit,c.Name as SupplierName,SUM(a.TotalPrice+(a.Tax - a.Discount))as Amount 
            
			from PosCloud.TransMaster as a 
			
			inner join PosCloud.TransDetails as b on a.id=b.TransMasterId
			inner join POsCloud.Products as p on b.ProductCode = p.ProductCode
            inner join PosCloud.BusinessPartners as c on a.BusinessPartnerId  = c.Id
			inner join PosCloud.Stores as d on a.StoreId=d.Id
            where a.Type = 'PRO' and c.Type = 'S' and a.StoreId in (" + string.Join(",", parameters.Where(a => a.ParameterName.Contains("d")).Select(p => "@" + p.ParameterName)) + @") and a.BusinessPartnerId = @p3
            group by a.TransCode,c.Name, d.Name,p.PurchaseUnit

            
           
                ";
            var data = _context.Database.SqlQuery<PurchaseReportViewModel>(sql, parameters.ToArray()).ToList();
            return data;
        }
        public List<SalesReportViewModel> GenerateTransactionsData(List<int> storeIds, DateTime dateFrom, DateTime dateTo)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-UK");
            IFormatProvider UK = new CultureInfo("en-UK").DateTimeFormat;
            dateFrom = Convert.ToDateTime(dateFrom, UK);
            dateTo = Convert.ToDateTime(dateTo, UK);
            var parameters = new List<SqlParameter> { new SqlParameter("@p3", dateFrom),
                new SqlParameter("@p4", dateTo),
            };

            parameters.AddRange(storeIds.Select((id, i) => new SqlParameter("d" + i, id)));


            var sql = @" select d.Name as BranchName,a.TransCode as InvoiceNumber,c.Name as SupplierName,SUM(a.TotalPrice+(a.Tax - a.Discount))as Amount 
            
			from PosCloud.TransMaster as a 
			
            inner join PosCloud.BusinessPartners as c on a.BusinessPartnerId  = c.Id
			inner join PosCloud.Stores as d on a.StoreId=d.Id
			
            where a.StoreId in (" + string.Join(",", parameters.Where(a => a.ParameterName.Contains("d")).Select(p => "@" + p.ParameterName)) + @") and a.TransDate >=@p3 and a.TransDate<=@p4
            group by a.TransCode,c.Name, d.Name
             
                ";

            return _context.Database.SqlQuery<SalesReportViewModel>(sql, parameters.ToArray()).ToList();
        }
        public List<BusinessPartnerViewModel> GenerateCustomersData(List<int> storeIds)
        {
            var parameters = new List<SqlParameter>();
            parameters.AddRange(storeIds.Select((id, i) => new SqlParameter("d" + i, id)));

            var sql = @" select d.Name as BranchName,a.Name as Name,a.Email as Email,a.PhoneNumber as PhoneNumber,a.Address as Address,a.City as City,a.State as State
            
			from PosCloud.BusinessPartners as a 
			
           
			inner join PosCloud.Stores as d on a.StoreId=d.Id
			
            where a.StoreId in (" + string.Join(",", parameters.Where(a => a.ParameterName.Contains("d")).Select(p => "@" + p.ParameterName)) + @") and a.Type = 'C' and a.IsDisabled = 0

            group by a.Name, d.Name,a.Email,a.PhoneNumber,a.Address,a.City,a.State

			
            
            
             
                ";

            return _context.Database.SqlQuery<BusinessPartnerViewModel>(sql, parameters.ToArray()).ToList();
        }

        public List<ProductCostReportViewModel> GenerateProductCostData(List<int> storeIds, List<string> productCodes)
        {
            var parameters = new List<SqlParameter> { };
            parameters.AddRange(storeIds.Select((id, i) => new SqlParameter("d" + i, id)));
            parameters.AddRange(productCodes.Select((id, i) => new SqlParameter("E" + i, id)));
            var sql = @"select d.Name as BranchName,a.Name as ProductName,SUM(a.CostPrice) as CostPrice
            
			from PosCloud.Products as a 
			
           
			inner join PosCloud.Stores as d on a.StoreId=d.Id
			
            where  a.IsDisabled = 0 and a.InventoryItem = 0 and a.Type !='Combo' and
            a.StoreId in (" + string.Join(",", parameters.Where(a => a.ParameterName.Contains("d")).Select(p => "@" + p.ParameterName)) + @") 
            and a.ProductCode in (" + string.Join(",", parameters.Where(a => a.ParameterName.Contains("E")).Select(p => "@" + p.ParameterName)) + @")  
            group by a.Name, d.Name,a.CostPrice
			
           
                ";
            var query = _context.Database.SqlQuery<ProductCostReportViewModel>(sql, parameters.ToArray()).ToList();
            return query;
        }

        public List<EmployeeShiftReportViewModel> GenerateEmployeeShiftData(List<int> storeIds, List<int> employeeIds, List<int> shiftIds)
        {
            var parameters = new List<SqlParameter> { };
            parameters.AddRange(storeIds.Select((id, i) => new SqlParameter("d" + i, id)));
            parameters.AddRange(employeeIds.Select((id, i) => new SqlParameter("E" + i, id)));
            parameters.AddRange(shiftIds.Select((id, i) => new SqlParameter("C" + i, id)));
            var sql = @" select d.Name as BranchName,a.Name as Shift,e.Name as EmployeeName
            
			from PosCloud.Shifts as a 
			
            inner join PosCloud.Employees as e on a.ShiftId = e.Id
			inner join PosCloud.Stores as d on a.StoreId=d.Id
			
            where a.StoreId	in (" + string.Join(",", parameters.Where(a => a.ParameterName.Contains("d")).Select(p => "@" + p.ParameterName)) + @") 
            and e.Id in (" + string.Join(",", parameters.Where(a => a.ParameterName.Contains("E")).Select(p => "@" + p.ParameterName)) + @") 
            and a.ShiftId in (" + string.Join(",", parameters.Where(a => a.ParameterName.Contains("C")).Select(p => "@" + p.ParameterName)) + @") 
            group by a.Name, d.Name,e.Name
			
           
            
                ";

            return _context.Database.SqlQuery<EmployeeShiftReportViewModel>(sql, parameters.ToArray()).ToList();
        }
        public List<TillOperationReportViewModel> GenerateTillOperationData(List<int> storeIds, DateTime dateFrom, DateTime dateTo)
        {


            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-UK");
            IFormatProvider UK = new CultureInfo("en-UK").DateTimeFormat;
            dateFrom = Convert.ToDateTime(dateFrom, UK);
            dateTo = Convert.ToDateTime(dateTo, UK);
            var parameters = new List<SqlParameter> { new SqlParameter("@p3", dateFrom),
                new SqlParameter("@p4", dateTo),
            };

            parameters.AddRange(storeIds.Select((id, i) => new SqlParameter("d" + i, id)));




            var sql = @"    select t.Name as BranchName,b.UserName as UserName,a.OperationDate as operationDate, a.OpeningAmount as OpeningAmount,
 
	 a.CarryOut as CarryOut
            , a.SystemAmount as SystemAmount,a.PhysicalAmount as ClosingAmount,
			s.Name as ShiftName,a.AdjustedCashAmount as AdjustedCashAmount,
			 a.AdjustedCreditAmount as AdjustedCreditAmount,a.AdjustedCreditNoteAmount as AdjustedCreditNoteAmount,
			(a.AdjustedCashAmount+a.AdjustedCreditAmount+a.AdjustedCreditNoteAmount)-(a.SystemAmount+ a.OpeningAmount) as Deficit
			
             From   PosCloud.TillOperations as a
             inner join PosCloud.Shifts as s on a.ShiftId = s.ShiftId
             inner join AspNetUsers as b on a.ApplicationUserId = b.Id
             inner join PosCloud.Stores as t on a.StoreId = s.StoreId
        
where a.StoreId in (" + string.Join(",", parameters.Where(a => a.ParameterName.Contains("d")).Select(p => "@" + p.ParameterName)) + @") and a.OperationDate >= @p3 and a.OperationDate <= @p4
         
             group by b.UserName,a.OperationDate,a.OpeningAmount,a.SystemAmount,a.PhysicalAmount,s.Name,t.Name, a.CarryOut ,
			 a.AdjustedCashAmount,a.AdjustedCreditAmount,a.AdjustedCreditNoteAmount
                ";

            return _context.Database.SqlQuery<TillOperationReportViewModel>(sql, parameters.ToArray()).ToList();
        }











        //public List<TillOperationReportViewModel> GenerateTillOperationData(List<int> storeIds, DateTime dateFrom, DateTime dateTo)
        //{


        //    Thread.CurrentThread.CurrentCulture = new CultureInfo("en-UK");
        //    IFormatProvider UK = new CultureInfo("en-UK").DateTimeFormat;
        //    dateFrom = Convert.ToDateTime(dateFrom, UK);
        //    dateTo = Convert.ToDateTime(dateTo, UK);
        //    var parameters = new List<SqlParameter> { new SqlParameter("@p3", dateFrom),
        //        new SqlParameter("@p4", dateTo),
        //    };

        //    parameters.AddRange(storeIds.Select((id, i) => new SqlParameter("d" + i, id)));




        //    var sql = @"   select t.Name as BranchName,b.Email as UserEmail,a.OperationDate as operationDate, a.OpeningAmount as OpeningAmount,
        //     a.SystemAmount as SystemAmount,a.PhysicalAmount as PhysicalAmount,s.Name as ShiftName,a.TillOperationType as TillOperationType
        //     From   PosCloud.TillOperations as a
        //     inner join PosCloud.Shifts as s on a.ShiftId = s.ShiftId
        //     inner join AspNetUsers as b on a.ApplicationUserId = b.Id
        //     inner join PosCloud.Stores as t on a.StoreId = s.StoreId
        //     where a.StoreId in (" + string.Join(",", parameters.Where(a => a.ParameterName.Contains("d")).Select(p => "@" + p.ParameterName)) + @") and a.OperationDate >= @p3 and a.OperationDate <= @p4
        //     group by b.Email,a.OperationDate,a.OpeningAmount,a.SystemAmount,a.PhysicalAmount,s.Name,a.TillOperationType,t.Name
        //        ";

        //    return _context.Database.SqlQuery<TillOperationReportViewModel>(sql, parameters.ToArray()).ToList();
        //}

        public List<SalesReportViewModel> GenerateConsumptionData(List<int> storeIds, DateTime dateFrom, DateTime dateTo)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-UK");
            IFormatProvider UK = new CultureInfo("en-UK").DateTimeFormat;
            dateFrom = Convert.ToDateTime(dateFrom, UK);
            dateTo = Convert.ToDateTime(dateTo, UK);
            var parameters = new List<SqlParameter> { new SqlParameter("@p3", dateFrom),
                new SqlParameter("@p4", dateTo),
            };

            parameters.AddRange(storeIds.Select((id, i) => new SqlParameter("d" + i, id)));


            var sql = @"  select p.Name as ProductName,d.Name as BranchName,a.TransCode as InvoiceNumber,SUM(b.Quantity) as Qty,p.IngredientUnit as IngredientUnit,SUM(b.UnitPrice*b.Quantity)as Amount,a.TransDate as Date 
            ,a.Discount as Discount,a.Tax as Tax
			from PosCloud.TransMaster as a 
            inner join PosCloud.TransDetails as b on a.id=b.TransMasterId
            inner join PosCloud.Products as p on b.ProductCode = p.ProductCode
			inner join PosCloud.Stores as d on a.StoreId=d.Id
            where a.StoreId  in (" + string.Join(",", parameters.Where(a => a.ParameterName.Contains("d")).Select(p => "@" + p.ParameterName)) + @") and a.TransDate >=@p3 and a.TransDate<=@p4 and a.Type= 'MIF'
            group by d.Name,a.TransCode,p.Name,a.TransCode,a.TransDate,a.Discount,a.Tax,p.IngredientUnit
                ";
            var query = _context.Database.SqlQuery<SalesReportViewModel>(sql, parameters.ToArray()).ToList();
            return query;
        }
        public List<VoidReasonsReportViewModel> GenerateVoidReasonsData(List<int> storeIds, DateTime dateFrom, DateTime dateTo)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-UK");
            IFormatProvider UK = new CultureInfo("en-UK").DateTimeFormat;
            dateFrom = Convert.ToDateTime(dateFrom, UK);
            dateTo = Convert.ToDateTime(dateTo, UK);
            var parameters = new List<SqlParameter> { new SqlParameter("@p3", dateFrom),
                new SqlParameter("@p4", dateTo),
            };

            parameters.AddRange(storeIds.Select((id, i) => new SqlParameter("d" + i, id)));



            var sql = @"   select d.Name as BranchName,a.TransCode as TransCode,SUM(b.Quantity) as Qty,SUM(b.UnitPrice*b.Quantity)as Amount,a.TransDate as Date 
            ,a.Description as VoidReasons,p.ProductCode as ProductCode
			from PosCloud.TransMaster as a 
            inner join PosCloud.TransDetails as b on a.id=b.TransMasterId
			inner join PosCloud.Products as p on b.ProductCode = p.ProductCode
			inner join PosCloud.Stores as d on a.StoreId=d.Id
            where a.StoreId in (" + string.Join(",", parameters.Where(a => a.ParameterName.Contains("d")).Select(p => "@" + p.ParameterName)) + @") and a.TransDate >=@p3 and a.TransDate<=@p4 and a.TransStatus = 'Void' 
            group by d.Name,a.TransCode,a.TransDate,a.Description,p.ProductCode
                ";
            return _context.Database.SqlQuery<VoidReasonsReportViewModel>(sql, parameters.ToArray()).ToList();
        }
        public List<ReturnReportViewModel> GenerateReturnData(List<int> storeIds, DateTime dateFrom, DateTime dateTo)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-UK");
            IFormatProvider UK = new CultureInfo("en-UK").DateTimeFormat;
            dateFrom = Convert.ToDateTime(dateFrom, UK);
            dateTo = Convert.ToDateTime(dateTo, UK);
            var parameters = new List<SqlParameter> { new SqlParameter("@p3", dateFrom),
                new SqlParameter("@p4", dateTo),
            };

            parameters.AddRange(storeIds.Select((id, i) => new SqlParameter("d" + i, id)));




            var sql = @"    select d.Name as BranchName,a.TransCode as InvoiceNumber,SUM(b.Quantity) as Qty,SUM(b.UnitPrice*b.Quantity)as Amount,a.TransDate as Date 
            ,p.Name as Products,a.TransStatus as Status
			from PosCloud.TransMaster as a 
            inner join PosCloud.TransDetails as b on a.id=b.TransMasterId
			inner join PosCloud.Products as p on b.ProductCode = p.ProductCode
			inner join PosCloud.Stores as d on a.StoreId=d.Id
            where a.StoreId in (" + string.Join(",", parameters.Where(a => a.ParameterName.Contains("d")).Select(p => "@" + p.ParameterName)) + @") and a.TransStatus = 'Returned' and a.Type = 'REF' and a.TransDate >=@p3 and a.TransDate<=@p4
            group by d.Name,a.TransCode,a.TransDate,a.Description,p.Name,a.TransStatus
           
                ";
            return _context.Database.SqlQuery<ReturnReportViewModel>(sql, parameters.ToArray()).ToList();
        }

        public List<TaxReportViewModel> GenerateTaxesData(List<int> storeIds, DateTime dateFrom, DateTime dateTo)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-UK");
            IFormatProvider UK = new CultureInfo("en-UK").DateTimeFormat;
            dateFrom = Convert.ToDateTime(dateFrom, UK);
            dateTo = Convert.ToDateTime(dateTo, UK);
            var parameters = new List<SqlParameter> { new SqlParameter("@p3", dateFrom),
                new SqlParameter("@p4", dateTo),
            };

            parameters.AddRange(storeIds.Select((id, i) => new SqlParameter("d" + i, id)));

            var sql = @"    Select s.Name as BranchName,a.Name as Name, a.Rate,ISNULL(a.IsPercentage,0) as Percentage From PosCloud.Taxes as a 
            inner join PosCloud.Stores as s on a.StoreId = s.Id
            where a.StoreId in (" + string.Join(",", parameters.Where(a => a.ParameterName.Contains("d")).Select(p => "@" + p.ParameterName)) + @") and a.CreatedOn >=@p3 and a.CreatedOn<=@p4 and a.IsDisabled=0
            group by a.Name, a.Rate,a.IsPercentage,s.Name
           
                ";
            return _context.Database.SqlQuery<TaxReportViewModel>(sql, parameters.ToArray()).ToList();
        }

        public List<OrderDiscountViewModel> GenerateOrderDiscountData(List<int> storeIds, DateTime dateFrom, DateTime dateTo)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-UK");
            IFormatProvider UK = new CultureInfo("en-UK").DateTimeFormat;
            dateFrom = Convert.ToDateTime(dateFrom, UK);
            dateTo = Convert.ToDateTime(dateTo, UK);
            var parameters = new List<SqlParameter> { new SqlParameter("@p3", dateFrom),
                new SqlParameter("@p4", dateTo),
            };

            parameters.AddRange(storeIds.Select((id, i) => new SqlParameter("d" + i, id)));

            var sql = @"select d.Name as BranchName,a.TransCode as InvoiceNumber,a.TransDate as Date 
            ,a.Discount as Discount
			from PosCloud.TransMaster as a 
            inner join PosCloud.TransDetails as b on a.id=b.TransMasterId
			inner join PosCloud.Stores as d on a.StoreId=d.Id
            where a.StoreId in (" + string.Join(",", parameters.Where(a => a.ParameterName.Contains("d")).Select(p => "@" + p.ParameterName)) + @") and Type= 'INV' and (a.TransStatus = 'Paid' or a.TransStatus = 'Complete') and a.TransDate >=@p3 and a.TransDate<=@p4 and a.Discount != 0
            group by d.Name,a.TransCode,a.TransCode,a.TransDate,a.Discount
            
           
                ";
            return _context.Database.SqlQuery<OrderDiscountViewModel>(sql, parameters.ToArray()).ToList();
        }

        public List<ProductDiscountViewModel> GenerateProductDiscountData(List<int> storeIds, DateTime dateFrom, DateTime dateTo)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-UK");
            IFormatProvider UK = new CultureInfo("en-UK").DateTimeFormat;
            dateFrom = Convert.ToDateTime(dateFrom, UK);
            dateTo = Convert.ToDateTime(dateTo, UK);
            var parameters = new List<SqlParameter> { new SqlParameter("@p3", dateFrom),
                new SqlParameter("@p4", dateTo),
            };

            parameters.AddRange(storeIds.Select((id, i) => new SqlParameter("d" + i, id)));

            var sql = @"select d.Name as BranchName,a.ProductCode as ProductCode,a.Name as ProductName, b.Discount as Discount,t.TransDate as Date
            
			from PosCloud.Products as a 
            inner join PosCloud.TransDetails as b on a.ProductCode=b.ProductCode
			inner join PosCloud.Stores as d on a.StoreId=d.Id
			inner join PosCloud.TransMaster as t on b.TransMasterId = t.Id
            where a.StoreId in (" + string.Join(",", parameters.Where(a => a.ParameterName.Contains("d")).Select(p => "@" + p.ParameterName)) + @") and b.DiscountId is not null and t.TransDate >=@p3 and t.TransDate<=@p4
            group by d.Name,a.Name,b.Discount,a.ProductCode,t.TransDate
            
           
                ";
            return _context.Database.SqlQuery<ProductDiscountViewModel>(sql, parameters.ToArray()).ToList();
        }

        public List<InventoryCostReportViewModel> GenerateItemsCostData(List<int> storeIds, List<string> productCodes,DateTime dateFrom, DateTime dateTo)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-UK");
            IFormatProvider UK = new CultureInfo("en-UK").DateTimeFormat;
            dateFrom = Convert.ToDateTime(dateFrom, UK);
            dateTo = Convert.ToDateTime(dateTo, UK);
            var parameters = new List<SqlParameter> { new SqlParameter("@p3", dateFrom),
                new SqlParameter("@p4", dateTo),
            };

            parameters.AddRange(storeIds.Select((id, i) => new SqlParameter("d" + i, id)));
            parameters.AddRange(productCodes.Select((id, i) => new SqlParameter("e" + i, id)));

            var sql = @"SELECT DISTINCT p.Name as ItemName,p.CostPrice as Cost,CONVERT(date, m.TransDate, 103) as Date
            From PosCloud.Products as p
            inner join PosCloud.TransDetails as t on p.ProductCode = t.ProductCode
            inner join PosCloud.TransMaster as m on t.TransMasterId = m.Id
            inner join PosCloud.Stores as s on p.StoreId = s.Id
            where p.InventoryItem = 1 and 
            p.StoreId in (" + string.Join(",", parameters.Where(a => a.ParameterName.Contains("d")).Select(p => "@" + p.ParameterName)) + @") and
            p.productCode in (" + string.Join(",", parameters.Where(a => a.ParameterName.Contains("e")).Select(p => "@" + p.ParameterName)) + @") 

            and m.TransDate >=@p3 and m.TransDate<=@p4 and p.IsDisabled = 0
             
            group by p.Name,p.CostPrice,CONVERT(date, m.TransDate, 103)
            
           
                ";
            return _context.Database.SqlQuery<InventoryCostReportViewModel>(sql, parameters.ToArray()).ToList();
        }
        public List<ExpirationReportViewModel> GenerateExpiryData(List<int> storeIds, DateTime dateFrom, DateTime dateTo)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-UK");
            IFormatProvider UK = new CultureInfo("en-UK").DateTimeFormat;
            dateFrom = Convert.ToDateTime(dateFrom, UK);
            dateTo = Convert.ToDateTime(dateTo, UK);

            var parameters = new List<SqlParameter> { new SqlParameter("@p3", dateFrom),
                new SqlParameter("@p4", dateTo),
            };

            parameters.AddRange(storeIds.Select((id, i) => new SqlParameter("d" + i, id)));
            var sql = @" select d.Name as BranchName,a.TransCode as TransCode,SUM(b.Quantity) as Qty,SUM(b.UnitPrice*b.Quantity)as Amount,a.TransDate as ExpiryDate 
            ,p.Name as Products
			from PosCloud.TransMaster as a 
            inner join PosCloud.TransDetails as b on a.id=b.TransMasterId
			inner join PosCloud.Products as p on b.ProductCode = p.ProductCode
			inner join PosCloud.Stores as d on a.StoreId=d.Id
             where  p.StoreId in (" + string.Join(",", parameters.Where(a => a.ParameterName.Contains("d")).Select(p => "@" + p.ParameterName)) + @") and  a.TransDate >=@p3 and  a.TransDate<=@p4 and a.Type = 'EXP'
            group by d.Name,a.TransCode,a.TransDate,p.Name
                ";

            var query = _context.Database.SqlQuery<ExpirationReportViewModel>(sql, parameters.ToArray()).ToList();
            return query;

        }

        public List<TransferReportViewModel> GenerateTransferData(List<int> storeIds, DateTime dateFrom, DateTime dateTo)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-UK");
            IFormatProvider UK = new CultureInfo("en-UK").DateTimeFormat;
            dateFrom = Convert.ToDateTime(dateFrom, UK);
            dateTo = Convert.ToDateTime(dateTo, UK);

            var parameters = new List<SqlParameter> { new SqlParameter("@p3", dateFrom),
                new SqlParameter("@p4", dateTo),
            };

            parameters.AddRange(storeIds.Select((id, i) => new SqlParameter("d" + i, id)));
            var sql = @" select d.Name as BranchName,a.TransCode as TransCode,a.TransferTo as TransferTo,SUM(b.Quantity) as Qty,SUM(b.UnitPrice*b.Quantity)as Amount,a.TransDate as Date 
            ,p.Name as Products
			from PosCloud.TransMaster as a 
            inner join PosCloud.TransDetails as b on a.id=b.TransMasterId
			inner join PosCloud.Products as p on b.ProductCode = p.ProductCode
			inner join PosCloud.Stores as d on a.StoreId=d.Id
             where  p.StoreId in (" + string.Join(",", parameters.Where(a => a.ParameterName.Contains("d")).Select(p => "@" + p.ParameterName)) + @") and  CONVERT(VARCHAR(20), a.TransDate, 101) >=@p3 and  CONVERT(VARCHAR(20), a.TransDate, 101)<=@p4 and a.Type = 'TRS'
            group by d.Name,a.TransCode,a.TransferTo,a.TransDate,p.Name
                ";
            return _context.Database.SqlQuery<TransferReportViewModel>(sql, parameters.ToArray()).ToList();
        }

        public List<StockTakingReportViewModel> GenerateStockTakingData(List<int> storeIds, DateTime dateFrom, DateTime dateTo)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-UK");
            IFormatProvider UK = new CultureInfo("en-UK").DateTimeFormat;
            dateFrom = Convert.ToDateTime(dateFrom, UK);
            dateTo = Convert.ToDateTime(dateTo, UK);
            var parameters = new List<SqlParameter> { new SqlParameter("@p3", dateFrom),
                new SqlParameter("@p4", dateTo),
            };

            parameters.AddRange(storeIds.Select((id, i) => new SqlParameter("d" + i, id)));
            var sql = @" select d.Name as BranchName,a.TransCode as TransCode,SUM(b.Quantity) as Qty,SUM(b.UnitPrice*b.Quantity)as Amount,a.TransDate as Date 
            ,p.Name as Products
			from PosCloud.TransMaster as a 
            inner join PosCloud.TransDetails as b on a.id=b.TransMasterId
			inner join PosCloud.Products as p on b.ProductCode = p.ProductCode
			inner join PosCloud.Stores as d on a.StoreId=d.Id
             where  p.StoreId in (" + string.Join(",", parameters.Where(a => a.ParameterName.Contains("d")).Select(p => "@" + p.ParameterName)) + @") and  CONVERT(VARCHAR(20), a.TransDate, 101) >=@p3 and  CONVERT(VARCHAR(20), a.TransDate, 101)<=@p4 and a.Type = 'STI'
            group by d.Name,a.TransCode,a.TransDate,p.Name
                ";
            return _context.Database.SqlQuery<StockTakingReportViewModel>(sql, parameters.ToArray()).ToList();
        }

    }
}
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

        public List<TransDetailViewModel> GenerateProductSalesData(int storeId,DateTime dateFrom,DateTime dateTo)
        {
            int[] transMasters = _context.TransMasters.Where(a=>a.StoreId==storeId && a.TransDate >= dateFrom && a.TransDate <= dateTo).Select(a => a.Id)
                .ToArray();
            List<TransDetailViewModel> transDetail = new List<TransDetailViewModel>();
            foreach (var transMaster in transMasters)
            {
                transDetail.AddRange(Mapper.Map<TransDetailViewModel[]>(_context.TransDetails.Where(a=>a.TransMasterId==transMaster && a.StoreId==storeId)));
            }
            foreach (var tempTransDetailViewModel in transDetail)
            {
                tempTransDetailViewModel.ProductName = _context.Products.FirstOrDefault(a =>
                    a.ProductCode == tempTransDetailViewModel.ProductCode && a.StoreId == tempTransDetailViewModel.StoreId).Name;
            }

            return transDetail;
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
    }
}
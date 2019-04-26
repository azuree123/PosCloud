using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using POSApp.Core.Models;

namespace POSApp.Core.ViewModels
{
    public class EmailFormViewModel
    {
        public string EmailTo { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Currency { get; set; }
        public int TotalOrders { get; set; }
        public decimal WeekBeforeSalesCompare { get; set; }
        public string EmployeeName { get; set; }
        public int TimeOrders { get; set; }
        public int EmployeeOrders { get; set; }
        public DateTime CurrentDate { get; set; }
        public DateTime DateWeekBefore { get; set; }
        public TimeSpan Time { get; set; }
        public string ClientName { get; set; }
        public decimal TimeSales { get; set; }
        [DefaultValue(0)]
        public decimal EmployeeSales { get; set; }
        [DefaultValue(0)]
        public decimal Sales { get; set; }
        public decimal WeeklySales { get; set; }
        [DefaultValue(0)]
        public double Expenses { get; set; }
        [DefaultValue(0)]
        public decimal PurchaseOrders { get; set; }
        [DefaultValue(0)]
        public decimal Refunds { get; set; }

        public List<StoreModel> StoreModels { get; set; }
        public List<TransDetailViewModel> TransDetailViewModels { get; set; } = new List<TransDetailViewModel>();
        public List<TransMasterViewModel> TransMasterViewModels { get; set; } = new List<TransMasterViewModel>();
        public List<Client> ClientApiViewModels { get; set; } = new List<Client>();
    }
   

    public class StoreModel
    {
        public int StoreId { get; set; }
        public string StoreName { get; set; }
    }
}
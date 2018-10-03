using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoSCloudApp.Core.Models
{
    public class SaleOrder:AuditableEntity
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int CustomerId { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public double Amount { get; set; }
        public int CashierId { get; set; }
        public double Tax { get; set; }
        public double Discount { get; set; }
        public string Status { get; set; }
        public bool Canceled { get; set; }
        public string Type { get; set; }

    }
}
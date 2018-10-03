using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoSCloudApp.Core.Models
{
    public class PurchaseOrder
    {
        public int Id { get; set; }
        public int SupplierId  { get; set; }
        public DateTime OrderDate  { get; set; }
        public DateTime SupplyDate { get; set; }
        public int InvoiceId { get; set; }
        public double TotalPrice { get; set; }
        public string Type { get; set; }

    }
}
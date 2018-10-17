using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace POSApp.Core.Models
{
    public class PurchaseOrder:AuditableEntity
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }

        public int SupplierId  { get; set; }
        public virtual Supplier Supplier { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime OrderDate  { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime SupplyDate { get; set; }
        public int InvoiceId { get; set; }
        [DefaultValue(0)]
        public double TotalPrice { get; set; }
        public string Type { get; set; }
        public virtual ICollection<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }
        

    }
}
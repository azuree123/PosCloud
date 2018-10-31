using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace POSApp.Core.Models
{
    public class AppCounter
    {
       
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public byte Id { get; set; }
            [DefaultValue(0)]
            public int InvoiceTransId { get; set; }
            [DefaultValue(0)]
            public int PurchaseTransId { get; set; }
            [DefaultValue(0)]
            public int Voucher { get; set; }
            [DefaultValue(0)]
            public int ProductId { get; set; }
            [DefaultValue(0)]
            public int SupplierId { get; set; }
            [DefaultValue(0)]
            public int CustomerId { get; set; }
            [DefaultValue(0)]
            public int FiscalYearId { get; set; }
        
    }
}
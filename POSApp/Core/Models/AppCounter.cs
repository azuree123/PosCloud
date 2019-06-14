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

            public int? DeviceId { get; set; }
            public Device Device { get; set; }
            public int? StoreId { get; set; }
            public Store Store { get; set; }
            [DefaultValue(0)]
            public int InvoiceTransId { get; set; }
            [DefaultValue(0)]
            public int HoldInvoiceTransId { get; set; }
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
            [DefaultValue(0)]
            public int MifId { get; set; }
            [DefaultValue(0)]
            public int STId { get; set; }
            [DefaultValue(0)]
            public int TransferId { get; set; }
            [DefaultValue(0)]
            public int PurchasingId { get; set; }
            [DefaultValue(0)]
            public int OtherInId { get; set; }
            [DefaultValue(0)]
            public int OtherOutId { get; set; }
            [DefaultValue(0)]
            public int ExpiryId { get; set; }
            [DefaultValue(0)]
            public int DamageId { get; set; }
            [DefaultValue(0)]
            public int WasteId { get; set; }
            [DefaultValue(0)]
            public int OpeningStockId { get; set; }
            [DefaultValue(0)]
            public int StockTakingId { get; set; }
    }
}
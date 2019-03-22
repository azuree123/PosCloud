using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using POSApp.Core.Models;
using POSApp.Core.Repositories;

namespace POSApp.Persistence.Repositories
{
    class AppCountersRepository : IAppCountersRepository
    {
        private readonly PosDbContext _context;

        public AppCountersRepository(PosDbContext context)
        {
            _context = context;
        }

        public int GetId(string dbSetIs)
        {
            int intId = 0;
            AppCounter appCounter = _context.AppCounters.ToList().FirstOrDefault();
            switch (dbSetIs)
            {
                case "Invoice":
                    intId = _context.AppCounters.DefaultIfEmpty().Max(r => r == null ? 0 : r.InvoiceTransId);
                    //intId = _context.AppCounters.Max(u => u.AutoAdId);
                    if (intId <= 0) { intId = 1; } else { intId = intId + 1; }
                    appCounter.InvoiceTransId = intId;
                    break;
                case "HoldInvoice":
                    intId = _context.AppCounters.DefaultIfEmpty().Max(r => r == null ? 0 : r.HoldInvoiceTransId);
                    //intId = _context.AppCounters.Max(u => u.AutoAdId);
                    if (intId <= 0) { intId = 1; } else { intId = intId + 1; }
                    appCounter.HoldInvoiceTransId = intId;
                    break;
                case "Purchase":
                    intId = _context.AppCounters.Max(u => u.PurchaseTransId);
                    if (intId <= 0) { intId = 1; } else { intId = intId + 1; }
                    appCounter.PurchaseTransId = intId;
                    break;
                case "Product":
                    intId = _context.AppCounters.Max(u => u.ProductId);
                    if (intId <= 0) { intId = 1; } else { intId = intId + 1; }
                    appCounter.ProductId = intId;
                    break;
                case "Supplier":
                    intId = _context.AppCounters.Max(u => u.SupplierId);
                    if (intId <= 0) { intId = 1; } else { intId = intId + 1; }
                    appCounter.SupplierId = intId;
                    break;
                case "Customer":
                    intId = _context.AppCounters.Max(u => u.CustomerId);
                    if (intId <= 0) { intId = 1; } else { intId = intId + 1; }
                    appCounter.CustomerId = intId;
                    break;
                case "FiscalYear":
                    intId = _context.AppCounters.Max(u => u.FiscalYearId);
                    if (intId <= 0) { intId = 1; } else { intId = intId + 1; }
                    appCounter.FiscalYearId = intId;
                    break;
                case "MIF":
                    intId = _context.AppCounters.Max(u => u.MifId);
                    if (intId <= 0) { intId = 1; } else { intId = intId + 1; }
                    appCounter.MifId = intId;
                    break;
                case "StockIn":
                    intId = _context.AppCounters.Max(u => u.STId);
                    if (intId <= 0) { intId = 1; } else { intId = intId + 1; }
                    appCounter.STId = intId;
                    break;
                case "Transfer":
                    intId = _context.AppCounters.Max(u => u.TransferId);
                    if (intId <= 0) { intId = 1; } else { intId = intId + 1; }
                    appCounter.TransferId = intId;
                    break;
               
                case "Purchasing":
                    intId = _context.AppCounters.Max(u => u.PurchasingId);
                    if (intId <= 0) { intId = 1; } else { intId = intId + 1; }
                    appCounter.PurchasingId = intId;
                    break;
                case "OtherIn":
                    intId = _context.AppCounters.Max(u => u.OtherInId);
                    if (intId <= 0) { intId = 1; } else { intId = intId + 1; }
                    appCounter.OtherInId = intId;
                    break;
                case "OtherOut":
                    intId = _context.AppCounters.Max(u => u.OtherOutId);
                    if (intId <= 0) { intId = 1; } else { intId = intId + 1; }
                    appCounter.OtherOutId = intId;
                    break;
                case "Expiry":
                    intId = _context.AppCounters.Max(u => u.ExpiryId);
                    if (intId <= 0) { intId = 1; } else { intId = intId + 1; }
                    appCounter.ExpiryId = intId;
                    break;
                case "Waste":
                    intId = _context.AppCounters.Max(u => u.WasteId);
                    if (intId <= 0) { intId = 1; } else { intId = intId + 1; }
                    appCounter.WasteId = intId;
                    break;
                case "Damage":
                    intId = _context.AppCounters.Max(u => u.DamageId);
                    if (intId <= 0) { intId = 1; } else { intId = intId + 1; }
                    appCounter.DamageId = intId;
                    break;
                case "OpeningStock":
                    intId = _context.AppCounters.Max(u => u.OpeningStockId);
                    if (intId <= 0) { intId = 1; } else { intId = intId + 1; }
                    appCounter.OpeningStockId = intId;
                    break;
            }

            //_context.SaveChanges();
            return intId;
        }
        //
        public void SetId(int intId, string dbSetIs)
        {

            AppCounter appCounter = _context.AppCounters.SingleOrDefault();
            switch (dbSetIs)
            {
                case "Invoice":
                    appCounter.InvoiceTransId = intId;
                    break;
                case "Purchase":
                    appCounter.PurchaseTransId = intId;
                    break;
                case "Product":
                    appCounter.ProductId = intId;
                    break;
                case "Supplier":
                    appCounter.SupplierId = intId;
                    break;
                case "Customer":
                    appCounter.CustomerId = intId;
                    break;
                case "FiscalYear":
                    appCounter.FiscalYearId = intId;
                    break;
                case "MIF":
                    appCounter.MifId = intId;
                    break;
                case "STI":
                    appCounter.STId = intId;
                    break;
                case "TRA":
                    appCounter.TransferId = intId;
                    break;
                case "OpeningStock":
                    appCounter.OpeningStockId = intId;
                    break;
            }

            //_context.SaveChanges();

        }

        public AppCounter GetAppCounter()
        {
            return _context.AppCounters.FirstOrDefault();
        }




    }
}

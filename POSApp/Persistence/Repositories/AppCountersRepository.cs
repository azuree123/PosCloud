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
            AppCounter appCounter = _context.AppCounters.SingleOrDefault();
            switch (dbSetIs)
            {
                case "Invoice":
                    intId = _context.AppCounters.DefaultIfEmpty().Max(r => r == null ? 0 : r.InvoiceTransId);
                    //intId = _context.AppCounters.Max(u => u.AutoAdId);
                    if (intId <= 0) { intId = 1; } else { intId = intId + 1; }
                    appCounter.InvoiceTransId = intId;
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

            }

            //_context.SaveChanges();

        }

        public AppCounter GetAppCounter()
        {
            return _context.AppCounters.FirstOrDefault();
        }




    }
}

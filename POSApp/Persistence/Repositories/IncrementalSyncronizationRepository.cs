using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using POSApp.Core.Models;
using POSApp.Core.Repositories;

namespace POSApp.Persistence.Repositories
{
    public class IncrementalSyncronizationRepository:IIncrementalSyncronizationRepository
    {

        private PosDbContext _context;

        public IncrementalSyncronizationRepository(PosDbContext context)
        {
            _context = context;
        }
        public void AddIncrementalSyncronization(IncrementalSyncronization incrementalSyncronization)
        {

            _context.IncrementalSyncronizations.Add(incrementalSyncronization);

        }
        public async Task<IncrementalSyncronization> GetLastIncrementalSyncronization(int storeId, int deviceId, string tableName)
        {

            return await _context.IncrementalSyncronizations.Where(a => a.DeviceId == deviceId && a.StoreId == storeId && a.TableName==tableName)
                .OrderByDescending(a => a.LastSynced).FirstOrDefaultAsync();

        }
    }
}
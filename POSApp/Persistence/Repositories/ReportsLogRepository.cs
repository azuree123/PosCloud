using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using POSApp.Core.Models;
using POSApp.Core.Repositories;

namespace POSApp.Persistence.Repositories
{
    public class ReportsLogRepository:IReportsLogRepository
    {
        private PosDbContext _context;

        public ReportsLogRepository(PosDbContext context)
        {
            _context = context;
        }
        public IEnumerable<ReportsLog> GetReportsLogs()
        {
            return _context.ReportsLogs.ToList();
        }
        public IEnumerable<ReportsLog> GetReportsLogs(string userId)
        {
            return _context.ReportsLogs.Where(a => a.CreatedById == userId).ToList();
        }

        public ReportsLog GetReportsLog(int id, int StoreId)
        {
            return _context.ReportsLogs.Find(id,StoreId);
        }

        public void AddReportsLog(ReportsLog ReportsLog)
        {
            _context.ReportsLogs.Add(ReportsLog);
        }

        public void UpdateReportsLog(int id, ReportsLog ReportsLog)
        {
            _context.ReportsLogs.Attach(ReportsLog);
            _context.Entry(ReportsLog).State = EntityState.Modified;
        }

        public void DeleteReportsLog(int id,int storeId)
        {
            var ReportsLog = new ReportsLog { Id = id,StoreId = storeId};
            _context.ReportsLogs.Attach(ReportsLog);
            _context.Entry(ReportsLog).State = EntityState.Deleted;
        }
        public IEnumerable<ReportsLog> GetApiReportsLogs()
        {
            IEnumerable<ReportsLog> ReportsLogs = _context.ReportsLogs.Where(a => !a.Synced).ToList();
            foreach (var ReportsLog in ReportsLogs)
            {
                ReportsLog.Synced = true;
                ReportsLog.SyncedOn = DateTime.Now;
            }

            _context.SaveChanges();
            return ReportsLogs;
        }
    }
}
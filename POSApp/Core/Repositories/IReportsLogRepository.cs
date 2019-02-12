using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POSApp.Core.Models;

namespace POSApp.Core.Repositories
{
   public interface IReportsLogRepository
   {
       IEnumerable<ReportsLog> GetReportsLogs();
       IEnumerable<ReportsLog> GetReportsLogs(string userId);
       ReportsLog GetReportsLog(int id, int StoreId);
       void AddReportsLog(ReportsLog ReportsLog);
       void UpdateReportsLog(int id, ReportsLog ReportsLog);
       void DeleteReportsLog(int id, int storeId);
       IEnumerable<ReportsLog> GetApiReportsLogs();
   }
}

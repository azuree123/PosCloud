using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POSApp.Core.Models;

namespace POSApp.Core.Repositories
{
    public interface IAppCountersRepository
    {
        int GetId(string dbSetIs);
        void SetId(int intId, string dbSetIs);
        AppCounter GetAppCounter();
        Task<IEnumerable<AppCounter>> GetAppCounterAsync(int storeId, int deviceId);
    }
}

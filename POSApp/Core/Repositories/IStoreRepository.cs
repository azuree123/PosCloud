using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POSApp.Core.Models;

namespace POSApp.Core.Repositories
{
    public interface IStoreRepository
    {
        IEnumerable<Store> GetStores();
        Store GetStoreById(int id);
        void AddStore(Store state);
        void UpdateStore(int id, Store state, int clientId);
        void DeleteStore(int id);
    }
}

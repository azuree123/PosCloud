using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using POSApp.Core.Models;

namespace POSApp.Core.Repositories
{
    public interface IWarehouseRepository
    {
        IEnumerable<Warehouse> GetWarehouses();
        Task<IEnumerable<Warehouse>> GetWarehousesAsync();
      
        Warehouse GetWarehouse(int id);
        Task<Warehouse> GetWarehouseAsync(int id);
        void AddWarehouse(Warehouse warehouse);
        Task AddWarehouseAsync(Warehouse warehouse);
        void UpdateWarehouse(int id, Warehouse warehouse);
        void DeleteWarehouse(int id);
    }
}
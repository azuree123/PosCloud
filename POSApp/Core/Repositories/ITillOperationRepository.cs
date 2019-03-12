using POSApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSApp.Core.Repositories
{
    public interface ITillOperationRepository
    {
        IEnumerable<TillOperation> GetTillOperations(int storeId);
        TillOperation GetTillOperationsById(int id, int storeId);
        void AddTillOperation(TillOperation to);
        void UpdateTillOperations(int id, int storeId, TillOperation to);
        void DeleteTillOperations(int id, int storeId);
        Task<IEnumerable<TillOperation>> GetTillOperationsAsync(int storeId);
        Task<TillOperation> GetTillOperationsByIdAsync(int id, int storeId);
        Task AddTillOperationAsync(TillOperation to);
        bool CheckTillOpened(string userId, int storeId);
        int GetTillSessionCode(string userId, int storeId);
        TillOperation GetOpenedTill(string userId, int storeId);
    }
}

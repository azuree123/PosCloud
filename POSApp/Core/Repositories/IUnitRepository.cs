using POSApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace POSApp.Core.Repositories
{
    public interface IUnitRepository
    {
        IEnumerable<Unit> GetUnit(int storeid);
        Unit GetUnitById(int id, int storeid);
        void AddUnit(Unit unit);
        void UpdateUnit(int id, Unit unit, int storeid);
        void DeleteUnit(int id, int storeId);
        Task<IEnumerable<Unit>> GetUnitAsync(int storeid);
        Task<Unit> GetUnitByIdAsync(int id, int storeid);
        Task AddUnitAsync(Unit unit);
    }
}
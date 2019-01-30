using System.Collections.Generic;
using System.Threading.Tasks;
using POSApp.Core.Models;

namespace POSApp.Core.Repositories
{
    public interface IDesignationRepository
    {
        IEnumerable<Designation> GetDesignations(int storeId);
        void AddDesignation(Designation Designation);
        Designation GetDesignationById(int id, int storeId);
        void UpdateDesignation(int id, int storeId, Designation Designation);
        void DeleteDesignation(int id, int storeId);
        IEnumerable<Designation> GetApiDesignations();
        Task<IEnumerable<Designation>> GetDesignationsAsync(int storeId);
        Task<Designation> GetDesignationByIdAsync(int id, int storeId);
        Task AddDesignationAsync(Designation Designation);
    }
}

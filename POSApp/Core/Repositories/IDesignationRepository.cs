using System.Collections.Generic;
using POSApp.Core.Models;

namespace POSApp.Core.Repositories
{
    public interface IDesignationRepository
    {
        IEnumerable<Designation> GetDesignations();
        void AddDesignation(Designation designation);
        Designation GetDesignationById(int id);
        void UpdateDesignation(int id, Designation designation);
        void DeleteDesignation(int id);
    }
}

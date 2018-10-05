using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoSCloudApp.Core.Models;

namespace PoSCloudApp.Core.Repositories
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

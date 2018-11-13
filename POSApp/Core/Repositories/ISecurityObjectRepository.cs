using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POSApp.Core.Models;

namespace POSApp.Core.Repositories
{
    public interface ISecurityObjectRepository
    {
        IEnumerable<SecurityObject> GetSecurityObjects();
        SecurityObject GetSecurityObject(int id);
        void AddSecurityObject(SecurityObject SecurityObject);
        void UpdateSecurityObject(int id, SecurityObject SecurityObject);
        void DeleteSecurityObject(int id);
    }
}

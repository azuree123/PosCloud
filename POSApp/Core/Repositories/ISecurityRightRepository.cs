using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POSApp.Core.Models;

namespace POSApp.Core.Repositories
{
    public interface ISecurityRightRepository
    {
        void AddSecurityRight(SecurityRight SecurityRight);
    }
}

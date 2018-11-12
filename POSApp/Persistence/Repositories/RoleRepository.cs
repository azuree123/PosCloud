using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using POSApp.Core.Models;

namespace POSApp.Persistence.Repositories
{
    public class RoleRepository
    {
        private PosDbContext _context;

        public RoleRepository(PosDbContext context)
        {
            _context = context;
        }

      
    }
}
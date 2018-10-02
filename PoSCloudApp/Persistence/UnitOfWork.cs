using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PoSCloud.Core;

namespace PoSCloud.Persistence
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly PosDbContext _context;
       

        public UnitOfWork(PosDbContext context)
        {
            _context = context;
            
        }

        public void Complete()
        {
            _context.SaveChanges();

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PoSCloud.Core;
using PoSCloudApp.Core.Repositories;
using PoSCloudApp.Persistence.Repositories;

namespace PoSCloud.Persistence
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly PosDbContext _context;

        public IStateRepository StateRepository { get;private set; }
        public ICityRepository CityRepository { get;private set; }
        public UnitOfWork(PosDbContext context)
        {
            _context = context;
            StateRepository=new StateRepository(context);
            CityRepository = new CityRepository(context);
        }

        public void Complete()
        {
            _context.SaveChanges();

        }
    }
}

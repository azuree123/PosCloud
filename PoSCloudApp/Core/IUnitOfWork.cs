using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoSCloud.Core
{
    public interface IUnitOfWork
    {
        

        void Complete();
    }
}

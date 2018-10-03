using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PoSCloudApp.Core.Repositories;

namespace PoSCloud.Core
{
    public interface IUnitOfWork
    {
        IStateRepository StateRepository { get; }
        ICityRepository CityRepository { get; }

        void Complete();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PoSCloudApp.Core.Models;

namespace PoSCloudApp.Core.Repositories
{
    public interface  IStateRepository
    {
        IEnumerable<State> GetStates();
        State GetStateById(int id);
        void UpdateState(int id, State state);
        void DeleteState(int id);
    }
}
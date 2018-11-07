using System.Collections.Generic;
using POSApp.Core.Models;

namespace POSApp.Core.Repositories
{
    public interface  IStateRepository
    {
        IEnumerable<State> GetStates();
        State GetStateById(int id);
        void AddState(State state);
        void UpdateState(int id, State state);
        void DeleteState(int id);
        IEnumerable<State> GetApiStates();
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
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
        Task<IEnumerable<State>> GetStatesAsync();
        Task<State> GetStateByIdAsync(int id);
        Task AddStateAsync(State state);
    }
}
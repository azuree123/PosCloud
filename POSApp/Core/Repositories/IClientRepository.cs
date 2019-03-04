using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POSApp.Core.Models;

namespace POSApp.Core.Repositories
{
    public interface IClientRepository
    {
        IEnumerable<Client> GetClients();
        Client GetClient(int id);
        void AddClient(Client client);
        void UpdateClient(int id, Client client);
        void DeleteClient(int id);
        Task<IEnumerable<Client>> GetClientsAsync();
        Task<Client> GetClientAsync(int id);
        Task AddClientAsync(Client client);
        IEnumerable<Store> GetClientStore(int id);
    }
}

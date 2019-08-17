using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POSApp.Core.Models;
using POSApp.Core.ViewModels;

namespace POSApp.Core.Repositories
{
    public interface IClientRepository
    {
        IEnumerable<Client> GetClients(int storeId);
        Client GetClient(int id);
        void AddClient(Client client);
        void UpdateClient(int id, Client client);
        void DeleteClient(int id);
        Task<IEnumerable<Client>> GetClientsAsync();
        Task<Client> GetClientAsync(int id);
        Task AddClientAsync(Client client);
        IEnumerable<Store> GetClientStore(int id);
        IEnumerable<UserStoreViewModel> GetUserStore(string id);
        IEnumerable<Warehouse> GetClientWarehouse(int id);
    }
}

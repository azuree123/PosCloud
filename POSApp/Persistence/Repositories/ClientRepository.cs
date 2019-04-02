using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using POSApp.Core.Models;
using POSApp.Core.Repositories;

namespace POSApp.Persistence.Repositories
{
    public class ClientRepository:IClientRepository
    {
        private PosDbContext _context;

        public ClientRepository(PosDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Client> GetClients()
        {
            return _context.Clients
                .Where(a => a.Stores.Any(b => b.Id==3))
                
                
                .Where(a=> !a.IsDisabled).ToList();
        }
        public async Task<IEnumerable<Client>> GetClientsAsync()
        {
            return await _context.Clients.Where(a => !a.IsDisabled).ToListAsync();
        }

        public Client GetClient(int id)
        {
            return _context.Stores.Include(a=>a.Client).Where(a=>a.Id==id).Select(a=>a.Client).FirstOrDefault();
           
        }
        public async Task<Client> GetClientAsync(int id)
        {
            return await _context.Clients.FindAsync(id);
        }
        public IEnumerable<Store> GetClientStore(int id)
        {
            return _context.Clients.Include(a=>a.Stores).Where(a=>a.Id==id).Select(a=>a.Stores).FirstOrDefault();
        }

      
        public void AddClient(Client client)
        {
            var inDb = _context.Clients.FirstOrDefault(a => a.Name == client.Name);
            if (inDb == null)
            {
                _context.Clients.Add(client);
            }
            else
            {
                if (inDb.IsDisabled)
                {
                    client.Id = inDb.Id;
                    _context.Entry(inDb).CurrentValues.SetValues(client);
                    _context.Entry(inDb).State = EntityState.Modified;
                }
                else
                {
                    throw new Exception("Entity Already Exists!");
                }
            }
        }
        public async Task AddClientAsync(Client client)
        {
            var inDb = await _context.Clients.FirstOrDefaultAsync(a => a.Name == client.Name);
            if (inDb == null)
            {
                _context.Clients.Add(client);
            }
            else
            {
                if (inDb.IsDisabled)
                {
                    client.Id = inDb.Id;
                    _context.Entry(inDb).CurrentValues.SetValues(client);
                    _context.Entry(inDb).State = EntityState.Modified;
                }
                else
                {
                    throw new Exception("Entity Already Exists!");
                }
            }
        }
        public void UpdateClient(int id, Client client)
        {

            _context.Clients.Attach(client);
            _context.Entry(client).State = EntityState.Modified;

           
        }

        public void DeleteClient(int id)
        {
            var client = _context.Clients.FirstOrDefault(a => a.Id == id);
            client.IsDisabled = true;
            _context.Clients.Attach(client);
            _context.Entry(client).State = EntityState.Modified;
        }
       
    }
}
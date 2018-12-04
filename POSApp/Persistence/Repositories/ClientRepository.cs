using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
            return _context.Clients.Where(a=>a.IsActive).ToList();
        }
        

        public Client GetClient(int id)
        {
            return _context.Clients.Find(id);
        }

        public void AddClient(Client client)
        {
            if (!_context.Clients.Where(a => a.Name == client.Name ).Any())
            {
            _context.Clients.Add(client);
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
            client.IsActive = false;
            _context.Clients.Attach(client);
            _context.Entry(client).State = EntityState.Modified;
        }
       
    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using POSApp.Core.Models;
using POSApp.Core.Repositories;
using POSApp.Core.ViewModels;
using POSApp.Core.ViewModels.Sync;
namespace POSApp.Persistence.Repositories
{
    public class StoreRepository:IStoreRepository
    {
        private PosDbContext _context;

        public StoreRepository(PosDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Store> GetStores()
        {
            return _context.Stores.Where(a=> !a.IsDisabled).ToList();
        }

        public Store GetStoreById(int id)
        {
            return _context.Stores.Find(id);
        }

        public void AddStore(Store store)
        {
            var inDb = _context.Stores.FirstOrDefault(a => a.Name == store.Name && a.Address == store.Address && a.City == store.City && a.State == store.State);
            if (inDb == null)
            {
                _context.Stores.Add(store);
            }
            else
            {
                if (inDb.IsDisabled)
                {
                    store.Id = inDb.Id;
                    _context.Entry(inDb).CurrentValues.SetValues(store);
                    _context.Entry(inDb).State = EntityState.Modified;
                }
                else
                {
                    throw new Exception("Entity Already Exists!");
                }
            }
        }

        public void UpdateStore(int id, Store store)
        {
            if (store.Id != id)
            {
                store.Id = id;
            }
            else
            {
                
            }

            _context.Stores.Attach(store);
            _context.Entry(store).State = EntityState.Modified;
        }

        public void DeleteStore(int id)
        {
            var store = _context.Stores.FirstOrDefault(a => a.Id == id);
            store.IsDisabled = true;
            _context.Stores.Attach(store);
            _context.Entry(store).State = EntityState.Modified;
        }
    }
}
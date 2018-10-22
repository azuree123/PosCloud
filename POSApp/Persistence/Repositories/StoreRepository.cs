﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.EntityFrameworkCore;
using POSApp.Core.Models;
using POSApp.Core.Repositories;
using EntityState = System.Data.Entity.EntityState;

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
            return _context.Stores.ToList();
        }

        public Store GetStoreById(int id)
        {
            return _context.Stores.Find(id);
        }

        public void AddStore(Store store)
        {
            _context.Stores.Add(store);
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
            var store = new Store {Id = id};
            _context.Stores.Attach(store);
            _context.Entry(store).State = EntityState.Deleted;
        }
    }
}
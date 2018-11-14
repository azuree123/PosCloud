using POSApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using POSApp.Core.Repositories;

namespace POSApp.Persistence.Repositories
{
    public class TillOperationRepository:ITillOperationRepository
    {
        private PosDbContext _context;

        public TillOperationRepository(PosDbContext context)
        {
            _context = context;
        }

        public IEnumerable<TillOperation> GetTillOperations(int storeId)
        {
            return _context.TillOperations.Where(x => x.StoreId == storeId).ToList();
        }

        public TillOperation GetTillOperationsById(int id, int storeId)
        {
            return _context.TillOperations.Where(a => a.Id == id && a.StoreId == storeId).ToList().FirstOrDefault();
        }

        public void AddTillOperation(TillOperation to)
        {
            if (!_context.TillOperations.Where(a => a.ApplicationUserId == to.ApplicationUserId && a.OperationDate == to.OperationDate && a.StoreId == to.StoreId).Any())
            {
                _context.TillOperations.Add(to);
            }
        }

        public void UpdateTillOperations(int id, int storeId, TillOperation to)
        {
            to.StoreId = storeId;
            _context.TillOperations.Attach(to);
            _context.Entry(to).State = EntityState.Modified;
        }

        public void DeleteTillOperations(int id, int storeId)
        {
            TillOperation to = new TillOperation { Id = id, StoreId = storeId };
            _context.TillOperations.Attach(to);
            _context.Entry(to).State = EntityState.Deleted;
        }
    }
}
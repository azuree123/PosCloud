using POSApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
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
            return _context.TillOperations.Include(a=>a.Shift).Where(x => x.StoreId == storeId && !x.IsDisabled).ToList();
        }
        public async Task<IEnumerable<TillOperation>> GetTillOperationsAsync(int storeId)
        {
            return await _context.TillOperations.Include(a => a.Shift).Where(x => x.StoreId == storeId && !x.IsDisabled).ToListAsync();
        }
        public TillOperation GetTillOperationsById(int id, int storeId)
        {
            return _context.TillOperations.Where(a => a.Id == id && a.StoreId == storeId).ToList().FirstOrDefault();
        }
        public TillOperation GetOpenedTill(string userId, int storeId)
        {
            return _context.TillOperations.Where(a => a.ApplicationUserId == userId && a.StoreId == storeId && a.TillOperationType.ToLower()=="open").ToList().OrderByDescending(a=>a.Id).FirstOrDefault();
        }
        public bool CheckTillOpened(string userId, int storeId)
        {
            return _context.TillOperations.Where(a => a.ApplicationUserId == userId && a.StoreId == storeId && a.TillOperationType.ToLower()=="open").Any();
        }
        public int GetTillSessionCode(string userId, int storeId)
        {
            return _context.TillOperations.Where(a => a.ApplicationUserId == userId && a.StoreId == storeId).Count()+1;
        }
        
        public async Task<TillOperation> GetTillOperationsByIdAsync(int id, int storeId)
        {
            return await _context.TillOperations.FirstOrDefaultAsync(a => a.Id == id && a.StoreId == storeId);
        }
        public void AddTillOperation(TillOperation to)
        {
            var inDb = _context.TillOperations.FirstOrDefault(a =>
                a.ApplicationUserId == to.ApplicationUserId && a.OperationDate == to.OperationDate &&
                a.StoreId == to.StoreId);
            if (inDb == null)
            {
                _context.TillOperations.Add(to);
            }
            else
            {
                if (inDb.IsDisabled)
                {
                    to.Id = inDb.Id;
                    _context.Entry(inDb).CurrentValues.SetValues(to);
                    _context.Entry(inDb).State = EntityState.Modified;
                }
                else
                {
                    throw new Exception("Entity Already Exists!");
                }
            }
        }
        public async Task AddTillOperationAsync(TillOperation to)
        {
            var inDb = await _context.TillOperations.FirstOrDefaultAsync(a =>
                a.ApplicationUserId == to.ApplicationUserId && a.OperationDate == to.OperationDate &&
                a.StoreId == to.StoreId);
            if (inDb == null)
            {
                _context.TillOperations.Add(to);
            }
            else
            {
                if (inDb.IsDisabled)
                {
                    to.Id = inDb.Id;
                    _context.Entry(inDb).CurrentValues.SetValues(to);
                    _context.Entry(inDb).State = EntityState.Modified;
                }
                else
                {
                    throw new Exception("Entity Already Exists!");
                }
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
            var tillOperation = _context.TillOperations.FirstOrDefault(a => a.Id == id && a.StoreId == storeId);
            tillOperation.IsDisabled = true;
            _context.TillOperations.Attach(tillOperation);
            _context.Entry(tillOperation).State = EntityState.Modified;
        }
        public IEnumerable<TillOperation> GetApiTillOperations()
        {
            IEnumerable<TillOperation> tillOperations = _context.TillOperations.Where(a => !a.Synced).ToList();
            foreach (var tillOperation in tillOperations)
            {
                tillOperation.Synced = true;
                tillOperation.SyncedOn = DateTime.Now;
            }

            _context.SaveChanges();
            return tillOperations;
        }
    }
}
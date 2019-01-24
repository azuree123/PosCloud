using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using POSApp.Core.Models;
using POSApp.Core.Repositories;

namespace POSApp.Persistence.Repositories
{
    public class ExpenseHeadRepository:IExpenseHeadRepository
    {
        private PosDbContext _context;

        public ExpenseHeadRepository(PosDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ExpenseHead> GetExpenseHeads(int storeId)
        {
            return _context.ExpenseHeads.Where(a=>a.StoreId == storeId && !a.IsDisabled).ToList();
        }
        public async Task<IEnumerable<ExpenseHead>> GetExpenseHeadsAsync(int storeId)
        {
            return await _context.ExpenseHeads.Where(a => a.StoreId == storeId && !a.IsDisabled).ToListAsync();
        }
        public ExpenseHead GetExpenseHeadById(int id, int storeid)
        {
            return _context.ExpenseHeads.Find(id,storeid);
        }
        public async Task<ExpenseHead> GetExpenseHeadByIdAsync(int id, int storeid)
        {
            return await _context.ExpenseHeads.FindAsync(id, storeid);
        }
        public void AddExpenseHead(ExpenseHead expenseHeads)
        {
            var inDb = _context.ExpenseHeads.FirstOrDefault(
                a => a.Name == expenseHeads.Name && a.StoreId == expenseHeads.StoreId);
            if (inDb == null)
            {
                _context.ExpenseHeads.Add(expenseHeads);
            }
            else
            {
                if (inDb.IsDisabled)
                {
                    expenseHeads.Id = inDb.Id;
                    _context.Entry(inDb).CurrentValues.SetValues(expenseHeads);
                    _context.Entry(inDb).State = EntityState.Modified;
                }
                else
                {
                    throw new Exception("Entity Already Exists!");
                }
            }
        }
        public async Task AddExpenseHeadAsync(ExpenseHead expenseHeads)
        {
            var inDb = await _context.ExpenseHeads.FirstOrDefaultAsync(
                a => a.Name == expenseHeads.Name && a.StoreId == expenseHeads.StoreId);
            if (inDb == null)
            {
                _context.ExpenseHeads.Add(expenseHeads);
            }
            else
            {
                if (inDb.IsDisabled)
                {
                    expenseHeads.Id = inDb.Id;
                    _context.Entry(inDb).CurrentValues.SetValues(expenseHeads);
                    _context.Entry(inDb).State = EntityState.Modified;
                }
                else
                {
                    throw new Exception("Entity Already Exists!");
                }
            }
        }
        public void UpdateExpenseHead(int id,int storeid, ExpenseHead expenseHeads)
        {
            expenseHeads.StoreId = storeid;
            _context.ExpenseHeads.Attach(expenseHeads);
            _context.Entry(expenseHeads).State = EntityState.Modified;
        }

        public void DeleteExpenseHead(int id, int storeid)
        {
            var expenseHead = _context.ExpenseHeads.FirstOrDefault(a => a.Id == id && a.StoreId == storeid);
            expenseHead.IsDisabled = true;
            _context.ExpenseHeads.Attach(expenseHead);
            _context.Entry(expenseHead).State = EntityState.Modified;
        }
        public IEnumerable<ExpenseHead> GetApiExpenseHeads()
        {
            IEnumerable<ExpenseHead> expenseHeads = _context.ExpenseHeads.Where(a => !a.Synced).ToList();
            foreach (var expenseHead in expenseHeads)
            {
                expenseHead.Synced = true;
                expenseHead.SyncedOn = DateTime.Now;
            }

            _context.SaveChanges();
            return expenseHeads;
        }
    }
}
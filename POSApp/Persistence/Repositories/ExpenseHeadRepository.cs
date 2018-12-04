using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
            return _context.ExpenseHeads.Where(a=>a.StoreId == storeId && a.IsActive).ToList();
        }

        public ExpenseHead GetExpenseHeadById(int id, int storeid)
        {
            return _context.ExpenseHeads.Find(id,storeid);
        }

        public void AddExpenseHead(ExpenseHead expenseHeads)
        {
            if (!_context.ExpenseHeads.Where(a => a.Name == expenseHeads.Name && a.StoreId == expenseHeads.StoreId).Any())
            {
            _context.ExpenseHeads.Add(expenseHeads);
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
            expenseHead.IsActive = false;
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
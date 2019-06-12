using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Threading.Tasks;
using POSApp.Core.Models;
using POSApp.Core.Repositories;

namespace POSApp.Persistence.Repositories
{
    public class ExpenseRepository:IExpenseRepository
    {
        private PosDbContext _context;

        public ExpenseRepository(PosDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Expense>> GetAllExpensesAsyncIncremental(int storeId, DateTime date)
        {
            return await _context.Expenses.Include(a => a.ExpenseHead).Where(a => a.StoreId == storeId && !a.IsDisabled && (a.UpdatedOn >= date || a.CreatedOn >= date)).ToListAsync();
        }
        public IEnumerable<Expense> GetExpenses(int storeId)
        {
            return _context.Expenses.Where(a=>a.StoreId==storeId && !a.IsDisabled).ToList();
        }
        public async Task<IEnumerable<Expense>> GetExpensesAsync(int storeId)
        {
            return await _context.Expenses.Where(a => a.StoreId == storeId && !a.IsDisabled).ToListAsync();
        }
        public IEnumerable<Expense> GetExpensesByDate(int stroreId)
        {
            return _context.Expenses.Where(a => a.Date == DateTime.Today && a.StoreId == stroreId && !a.IsDisabled).ToList();
        }
        public Expense GetExpenseById(int id, int storeid)
        {
            return _context.Expenses.Find(id,storeid);
        }
        public async Task<Expense> GetExpenseByIdAsync(int id, int storeid)
        {
            return await _context.Expenses.FindAsync(id, storeid);
        }
        public void AddExpense(Expense expense)
        {
            
                _context.Expenses.Add(expense);
            
        }
        public async Task AddExpenseAsync(Expense expense)
        {

            _context.Expenses.Add(expense);

        }
        public void UpdateExpense(int id, Expense expense,int storeid)
        {
            expense.StoreId = storeid;
            _context.Expenses.Attach(expense);
            _context.Entry(expense).State = EntityState.Modified;
        }

        public void DeleteExpense(int id, int storeid)
        {
            var expense = _context.Expenses.FirstOrDefault(a => a.Id == id && a.StoreId == storeid);
            expense.IsDisabled = true;
            _context.Expenses.Attach(expense);
            _context.Entry(expense).State = EntityState.Modified;
        }
        public IEnumerable<Expense> GetApiExpenses()
        {
            IEnumerable<Expense> expenses = _context.Expenses.Where(a => !a.Synced).ToList();
            foreach (var expense in expenses)
            {
                expense.Synced = true;
                expense.SyncedOn = DateTime.Now;
            }

            _context.SaveChanges();
            return expenses;
        }
    }
}
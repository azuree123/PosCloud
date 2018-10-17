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

        public IEnumerable<ExpenseHead> GetExpenseHeads()
        {
            return _context.ExpenseHeads.ToList();
        }

        public ExpenseHead GetExpenseHeadById(int id, int storeid)
        {
            return _context.ExpenseHeads.Find(id,storeid);
        }

        public void AddExpenseHead(ExpenseHead expenseHeads)
        {
            _context.ExpenseHeads.Add(expenseHeads);
        }

        public void UpdateExpenseHead(int id,int storeid, ExpenseHead expenseHeads)
        {
            expenseHeads.StoreId = storeid;
            _context.ExpenseHeads.Attach(expenseHeads);
            _context.Entry(expenseHeads).State = EntityState.Modified;
        }

        public void DeleteExpenseHead(int id, int storeid)
        {
            var expenseHeads = new ExpenseHead { Id = id,StoreId = storeid};
            _context.ExpenseHeads.Attach(expenseHeads);
            _context.Entry(expenseHeads).State = EntityState.Deleted;
        }
    }
}
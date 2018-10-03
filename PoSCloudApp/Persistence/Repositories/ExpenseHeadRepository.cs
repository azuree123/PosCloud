using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using PoSCloud.Persistence;
using PoSCloudApp.Core.Models;
using PoSCloudApp.Core.Repositories;

namespace PoSCloudApp.Persistence.Repositories
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

        public ExpenseHead GetExpenseHeadById(int id)
        {
            return _context.ExpenseHeads.Find(id);
        }

        public void AddExpenseHead(ExpenseHead expenseHeads)
        {
            _context.ExpenseHeads.Add(expenseHeads);
        }

        public void UpdateExpenseHead(int id, ExpenseHead expenseHeads)
        {
            _context.ExpenseHeads.Attach(expenseHeads);
            _context.Entry(expenseHeads).State = EntityState.Modified;
        }

        public void DeleteExpenseHead(int id)
        {
            var expenseHeads = new ExpenseHead { Id = id };
            _context.ExpenseHeads.Attach(expenseHeads);
            _context.Entry(expenseHeads).State = EntityState.Deleted;
        }
    }
}
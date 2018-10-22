﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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

        public IEnumerable<Expense> GetExpenses()
        {
            return _context.Expenses.ToList();
        }

        public Expense GetExpenseById(int id, int storeid)
        {
            return _context.Expenses.Find(id,storeid);
        }

        public void AddExpense(Expense expense)
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
            var expense = new Expense { Id = id, StoreId = storeid};
            _context.Expenses.Attach(expense);
            _context.Entry(expense).State = EntityState.Deleted;
        }
    }
}
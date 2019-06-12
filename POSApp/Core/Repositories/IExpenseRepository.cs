using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using POSApp.Core.Models;

namespace POSApp.Core.Repositories
{
    public interface IExpenseRepository
    {
        IEnumerable<Expense> GetExpenses(int storeId);
        Expense GetExpenseById(int id,int storeid);
        void AddExpense(Expense expense);
        void UpdateExpense(int id, Expense expense,int storeid);
        void DeleteExpense(int id,int storeid);
        IEnumerable<Expense> GetExpensesByDate(int stroreId);
        IEnumerable<Expense> GetApiExpenses();
        Task<IEnumerable<Expense>> GetExpensesAsync(int storeId);
        Task<Expense> GetExpenseByIdAsync(int id, int storeid);
        Task AddExpenseAsync(Expense expense);
        Task<IEnumerable<Expense>> GetAllExpensesAsyncIncremental(int storeId, DateTime date);
    }
}

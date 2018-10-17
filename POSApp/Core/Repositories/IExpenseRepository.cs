using System.Collections.Generic;
using POSApp.Core.Models;

namespace POSApp.Core.Repositories
{
    public interface IExpenseRepository
    {
        IEnumerable<Expense> GetExpenses();
        Expense GetExpenseById(int id);
        void AddExpense(Expense expense);
        void UpdateExpense(int id, Expense expense);
        void DeleteExpense(int id);
    }
}

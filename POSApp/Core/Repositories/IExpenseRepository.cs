using System.Collections.Generic;
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
        IEnumerable<Expense> GetApiExpenses();
    }
}

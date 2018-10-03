using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoSCloudApp.Core.Models;

namespace PoSCloudApp.Core.Repositories
{
    public interface IExpenseRepository
    {
        IEnumerable<Expense> GetExpenses();
        Expense GetExpenseById(int id);
        void UpdateExpense(int id, Expense expense);
        void DeleteExpense(int id);
    }
}

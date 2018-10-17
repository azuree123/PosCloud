using System.Collections.Generic;
using POSApp.Core.Models;

namespace POSApp.Core.Repositories
{
    public interface IExpenseHeadRepository
    {
        IEnumerable<ExpenseHead> GetExpenseHeads();
        ExpenseHead GetExpenseHeadById(int id, int storeid);
        void AddExpenseHead(ExpenseHead expenseHeads);
        
        void UpdateExpenseHead(int id,int storeid, ExpenseHead expenseHead);
        void DeleteExpenseHead(int id,int storeid);
    }
}

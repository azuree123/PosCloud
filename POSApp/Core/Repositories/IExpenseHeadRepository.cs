using System.Collections.Generic;
using System.Threading.Tasks;
using POSApp.Core.Models;

namespace POSApp.Core.Repositories
{
    public interface IExpenseHeadRepository
    {
        IEnumerable<ExpenseHead> GetExpenseHeads(int storeId);
        ExpenseHead GetExpenseHeadById(int id, int storeid);
        void AddExpenseHead(ExpenseHead expenseHeads);
        
        void UpdateExpenseHead(int id,int storeid, ExpenseHead expenseHead);
        void DeleteExpenseHead(int id,int storeid);
        IEnumerable<ExpenseHead> GetApiExpenseHeads();
        Task<IEnumerable<ExpenseHead>> GetExpenseHeadsAsync(int storeId);
        Task<ExpenseHead> GetExpenseHeadByIdAsync(int id, int storeid);
        Task AddExpenseHeadAsync(ExpenseHead expenseHeads);
    }
}

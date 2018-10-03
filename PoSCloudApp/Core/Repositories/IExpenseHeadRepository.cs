using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoSCloudApp.Core.Models;

namespace PoSCloudApp.Core.Repositories
{
    public interface IExpenseHeadRepository
    {
        IEnumerable<ExpenseHead> GetExpenseHeads();
        ExpenseHead GetExpenseHeadById(int id);
        void UpdateExpenseHead(int id, ExpenseHead expenseHead);
        void DeleteExpenseHead(int id);
    }
}

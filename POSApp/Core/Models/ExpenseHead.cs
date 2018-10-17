using System.Collections.Generic;

namespace POSApp.Core.Models
{
    public class ExpenseHead:AuditableEntity
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public virtual ICollection<Expense> Expenses { get; set; }
    }
}
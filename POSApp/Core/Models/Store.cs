using System.Collections.Generic;

namespace POSApp.Core.Models
{
    public class Store : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public string Contact { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public bool IsOperational { get; set; }

        public virtual ICollection<BusinessPartner> BusinessPartners { get; set; }
        public virtual ICollection<TransMaster> TransMasters { get; set; }
        public virtual ICollection<TransDetail> TransDetails { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Expense> Expenses { get; set; }
        public virtual ICollection<ExpenseHead> ExpenseHeads { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }
        public virtual ICollection<SaleOrder> SaleOrders { get; set; }
        public virtual ICollection<Supplier> Suppliers { get; set; }
        public virtual ICollection<Coupon> Coupons { get; set; }
        public virtual ICollection<Tax> Taxes { get; set; }
        public virtual ICollection<Discount> Discounts { get; set; }
        public virtual ICollection<Unit> Units { get; set; }
        public virtual ICollection<ReportsLog> ReportsLogs { get; set; }
        public virtual ICollection<Modifier> Modifiers { get; set; }
        public virtual ICollection<ModifierOption> ModifierOptions { get; set; }

        public virtual ICollection<TimedEvent> TimedEvents { get; set; }
        public virtual ICollection<ProductsSub> ProductsSubs { get; set; }
        public virtual ICollection<TimedEventProducts> TimedEventProducts { get; set; }



    }
}
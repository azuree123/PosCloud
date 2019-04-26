using System;
using System.Collections.Generic;

namespace POSApp.Core.Models
{
    public class Store : AuditableEntity
    {
        public int Id { get; set; }

       // public string Reference { get; set; } //code
        public int? ClientId { get; set; }
        public Client Client { get; set; }
        public string Name { get; set; }
        public string ArabicName { get; set; }
        public string Address { get; set; }
        
        public string Contact { get; set; }
        public DateTime BusinessStartTime { get; set; }
        public string Currency { get; set; }
        public string City { get; set; }

        public string State { get; set; }

        public bool IsOperational { get; set; }

        public virtual ICollection<BusinessPartner> BusinessPartners { get; set; }
        public virtual ICollection<TransMaster> TransMasters { get; set; }
        public virtual ICollection<TransDetail> TransDetails { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Expense> Expenses { get; set; }
        public virtual ICollection<ExpenseHead> ExpenseHeads { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        //public virtual ICollection<Coupon> Coupons { get; set; }
        public virtual ICollection<Tax> Taxes { get; set; }
        public virtual ICollection<Discount> Discounts { get; set; }
        public virtual ICollection<Unit> Units { get; set; }
        public virtual ICollection<ReportsLog> ReportsLogs { get; set; }
        public virtual ICollection<Modifier> Modifiers { get; set; }
        public virtual ICollection<ModifierOption> ModifierOptions { get; set; }

        public virtual ICollection<TimedEvent> TimedEvents { get; set; }
        public virtual ICollection<ProductsSub> ProductsSubs { get; set; }
        public virtual ICollection<TimedEventProducts> TimedEventProducts { get; set; }
        public virtual ICollection<Floor> Floors { get; set; }
        public virtual ICollection<DineTable> DineTables { get; set; }
        public virtual ICollection<Device> Devices { get; set; }
        public virtual ICollection<SecurityRight> SecurityRights { get; set; }
        public virtual ICollection<ModifierLinkProduct> ProductsLink { get; set; }
        public virtual ICollection<ModifierLinkProduct> ModifiersLink { get; set; }
        public virtual ICollection<ModifierTransDetail> ModifierTransDetails { get; set; }
        public virtual ICollection<Recipe> Recipes { get; set; }

        public virtual ICollection<Warehouse> Warehouses { get; set; }
        public virtual ICollection<ComboProductsTransDetail> ComboProductsTransDetails { get; set; }
        public virtual ICollection<AppCounter> AppCounters { get; set; }=new List<AppCounter>();
        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; } =new List<ApplicationUser>();

    }
}
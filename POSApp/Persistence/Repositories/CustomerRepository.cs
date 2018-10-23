using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using POSApp.Core.Models;
using POSApp.Core.Repositories;

namespace POSApp.Persistence.Repositories
{
    public class CustomerRepository :ICustomerRepository
    {
        private PosDbContext _context;

        public CustomerRepository(PosDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return _context.Customers.ToList();
        }

        public Customer GetCustomerById(int id, int storeid)
        {
            return _context.Customers.Find(id,storeid);
        }

        public void AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
        }

        public void UpdateCustomer(int id, int storeid, Customer customer)
        {
            if (customer.Id != id)
            {
                customer.Id = id;
            }
            else
            {
                
            }

            customer.StoreId = storeid;
            _context.Customers.Attach(customer);
            _context.Entry(customer).State = EntityState.Modified;
        }
        public void DeleteCustomer(int id,int storeid)
        {
            var customer = new Customer {Id = id,StoreId = storeid};
            _context.Customers.Attach(customer);
            _context.Entry(customer).State = EntityState.Deleted;
        }
        public IEnumerable<Customer> GetApiCustomers()
        {
            IEnumerable<Customer> customers = _context.Customers.Where(a => !a.Synced).ToList();
            foreach (var customer in customers)
            {
                customer.Synced = true;
                customer.SyncedOn = DateTime.Now;
            }

            _context.SaveChanges();
            return customers;
        }
    }
}
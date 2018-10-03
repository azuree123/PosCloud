using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using PoSCloud.Persistence;
using PoSCloudApp.Core.Models;

namespace PoSCloudApp.Persistence.Repositories
{
    public class CustomerRepository
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

        public Customer GetCustomerById(int id)
        {
            return _context.Customers.Find(id);
        }

        public void AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
        }

        public void UpdateCustomer(int id, Customer customer)
        {
            if (customer.Id != id)
            {
                customer.Id = id;
            }
            else
            {
                
            }

            _context.Customers.Attach(customer);
            _context.Entry(customer).State = EntityState.Modified;
        }
        public void DeleteCustomer(int id)
        {
            var customer = new Customer {Id = id};
            _context.Customers.Attach(customer);
            _context.Entry(customer).State = EntityState.Deleted;
        }
    }
}
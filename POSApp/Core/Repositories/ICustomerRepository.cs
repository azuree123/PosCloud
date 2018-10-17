using System.Collections.Generic;
using POSApp.Core.Models;

namespace POSApp.Core.Repositories
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetCustomers();
        Customer GetCustomerById(int id,int storeid);
        void AddCustomer(Customer customer);
        void UpdateCustomer(int id,int storeid, Customer customer);
        void DeleteCustomer(int id, int storeid);
    }
}

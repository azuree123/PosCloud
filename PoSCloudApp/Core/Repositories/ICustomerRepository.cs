using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoSCloudApp.Core.Models;

namespace PoSCloudApp.Core.Repositories
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetCustomers();
        Customer GetCustomerById(int id);
        void AddCustomer(Customer customer);
        void UpdateCustomer(int id, Customer customer);
        void DeleteCustomer(int id);
    }
}

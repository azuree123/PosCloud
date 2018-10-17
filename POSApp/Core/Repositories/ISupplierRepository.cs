using System.Collections.Generic;
using POSApp.Core.Models;

namespace POSApp.Core.Repositories
{
    public interface ISupplierRepository
    {
        IEnumerable<Supplier> GetSuppliers();
        Supplier GetSupplierById(int id);
        void AddSupplier(Supplier supplier);
        void UpdateSupplier(int id, Supplier supplier);
        void DeleteSupplier(int id);
    }
}

using System.Collections.Generic;
using POSApp.Core.Models;

namespace POSApp.Core.Repositories
{
    public interface ISupplierRepository
    {
        IEnumerable<Supplier> GetSuppliers();
        Supplier GetSupplierById(int id,int storeid);
        void AddSupplier(Supplier supplier);
        void UpdateSupplier(int id,int storeid ,Supplier supplier);
        void DeleteSupplier(int id, int storeid);
    }
}

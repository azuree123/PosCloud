using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using POSApp.Core.Models;
using POSApp.Core.Repositories;

namespace POSApp.Persistence.Repositories
{
    public class SupplierRepository:ISupplierRepository
    {
        private PosDbContext _context;

        public SupplierRepository(PosDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Supplier> GetSuppliers()
        {
            return _context.Suppliers.ToList();
        }

        public Supplier GetSupplierById(int id, int storeid)
        {
            return _context.Suppliers.Find(id,storeid);
        }

        public void AddSupplier(Supplier supplier)
        {
            _context.Suppliers.Add(supplier);
        }

        public void UpdateSupplier(int id, int storeid ,Supplier supplier)
        {
            supplier.StoreId = storeid;
            _context.Suppliers.Attach(supplier);
            _context.Entry(supplier).State = EntityState.Modified;
        }

        public void DeleteSupplier(int id, int storeid)
        {
            var supplier = new Supplier { Id = id, StoreId = storeid};
            _context.Suppliers.Attach(supplier);
            _context.Entry(supplier).State = EntityState.Deleted;
        }
    }
}
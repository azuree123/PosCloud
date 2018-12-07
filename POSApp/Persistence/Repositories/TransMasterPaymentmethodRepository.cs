using POSApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using POSApp.Core.Repositories;

namespace POSApp.Persistence.Repositories
{
    public class TransMasterPaymentMethodRepository : ITransMasterPaymentMethodRepository
    {
        private PosDbContext _context;

        public TransMasterPaymentMethodRepository(PosDbContext context)
        {
            _context = context;
        }

        public IEnumerable<TransMasterPaymentMethod> GetTransMasterPaymentMethods(int storeid)
        {
            return _context.TransMasterPaymentMethods.Where(a => a.StoreId == storeid && !a.IsDisabled).ToList();
        }

        public TransMasterPaymentMethod GetTransMasterPaymentMethodById(int id, int storeid)
        {
            return _context.TransMasterPaymentMethods.Find(id, storeid);
        }

        public void AddTransMasterPaymentMethod(TransMasterPaymentMethod TransMasterPaymentMethod)
        {
            
                _context.TransMasterPaymentMethods.Add(TransMasterPaymentMethod);
        }

        public void UpdateTransMasterPaymentMethod(int id, TransMasterPaymentMethod TransMasterPaymentMethod, int storeid)
        {
            if (TransMasterPaymentMethod.Id != id)
            {
                TransMasterPaymentMethod.Id = id;
            }
            else { }

            TransMasterPaymentMethod.StoreId = storeid;
            _context.TransMasterPaymentMethods.Attach(TransMasterPaymentMethod);
            _context.Entry(TransMasterPaymentMethod).State = EntityState.Modified;
        }

        public void DeleteTransMasterPaymentMethod(int id, int storeid)
        {
            var dinetable = _context.TransMasterPaymentMethods.FirstOrDefault(a => a.Id == id && a.StoreId == storeid);
            dinetable.IsDisabled = true;
            _context.TransMasterPaymentMethods.Attach(dinetable);
            _context.Entry(dinetable).State = EntityState.Modified;
        }
    }
}
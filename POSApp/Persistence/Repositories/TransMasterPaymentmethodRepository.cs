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
        public IEnumerable<TransMasterPaymentMethod> GetPaymentMethods()
        {
            return _context.TransMasterPaymentMethods.Include(a => a.TransMaster).Where(a => !a.IsDisabled).ToList();
        }
        public IEnumerable<TransMasterPaymentMethod> GetPaymentMethods(int transMasterId)
        {
            return _context.TransMasterPaymentMethods.Where(a => a.TransMasterId == transMasterId && !a.IsDisabled).Include(a => a.TransMaster).ToList();
        }

        public TransMasterPaymentMethod GetTransMasterPaymentMethod(int id)
        {
            return _context.TransMasterPaymentMethods.Find(id);
        }

        public void AddTransMasterPaymentMethod(TransMasterPaymentMethod transMasterPaymentMethod)
        {
                _context.TransMasterPaymentMethods.Add(transMasterPaymentMethod);
        }

        public void UpdateTransMasterPaymentMethod(int id, TransMasterPaymentMethod transMasterPaymentMethod)
        {
            _context.TransMasterPaymentMethods.Attach(transMasterPaymentMethod);
            _context.Entry(transMasterPaymentMethod).State = EntityState.Modified;
        }

        public void DeleteTransMasterPaymentMethod(int id)
        {
            var transMasterPaymentMethod = _context.TransMasterPaymentMethods.FirstOrDefault(a => a.Id == id);
            transMasterPaymentMethod.IsDisabled = true;
            _context.TransMasterPaymentMethods.Attach(transMasterPaymentMethod);
            _context.Entry(transMasterPaymentMethod).State = EntityState.Modified;
        }
       
    }
}
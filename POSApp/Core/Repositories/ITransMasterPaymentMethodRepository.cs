using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using POSApp.Core.Models;

namespace POSApp.Core.Repositories
{
    public interface ITransMasterPaymentMethodRepository
    {
        IEnumerable<TransMasterPaymentMethod> GetPaymentMethods();
        IEnumerable<TransMasterPaymentMethod> GetPaymentMethods(int transMasterId);
        TransMasterPaymentMethod GetTransMasterPaymentMethod(int id);
        void AddTransMasterPaymentMethod(TransMasterPaymentMethod transMasterPaymentMethod);
        void UpdateTransMasterPaymentMethod(int id, TransMasterPaymentMethod transMasterPaymentMethod);
        void DeleteTransMasterPaymentMethod(int id);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using POSApp.Core.Models;

namespace POSApp.Core.Repositories
{
    public interface ITransMasterPaymentMethodRepository
    {
        IEnumerable<TransMasterPaymentMethod> GetTransMasterPaymentMethods(int storeid);
        TransMasterPaymentMethod GetTransMasterPaymentMethodById(int id, int storeid);
        void AddTransMasterPaymentMethod(TransMasterPaymentMethod TransMasterPaymentMethod);
        void UpdateTransMasterPaymentMethod(int id, TransMasterPaymentMethod TransMasterPaymentMethod, int storeid);
        void DeleteTransMasterPaymentMethod(int id, int storeid);
    }
}
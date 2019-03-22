using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POSApp.Core.Models;

namespace POSApp.Core.Repositories
{
   public interface IComboProductTransDetailRepository
    {
        IEnumerable<ComboProductsTransDetail> GetComboProductTransDetail(int id, int storeId);
        ComboProductsTransDetail GetComboProductTransDetailById(int id, int storeId);
        void AddComboProductTransDetail(ComboProductsTransDetail tep);
        void UpdateComboProductTransDetail(int id, int transDetailId, ComboProductsTransDetail tep, int storeId);
        void DeleteComboProductTransDetail(int id, int storeId);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POSApp.Core.Models;

namespace POSApp.Core.Repositories
{
   public interface IModifierTransDetailRepository
   {
       IEnumerable<ModifierTransDetail> GeModifierTransDetails(int id, int storeId);
       ModifierTransDetail GetModifierTransDetailById(int id, int storeId);
       void AddModifierTransDetail(ModifierTransDetail tep);
       void UpdateModifierTransDetail(int id, int transDetailId, ModifierTransDetail tep, int storeId);
       void DeleteModifierTransDetail(int id, int storeId);
   }
}

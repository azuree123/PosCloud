using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POSApp.Core.Models;

namespace POSApp.Core.Repositories
{
   public interface IModifierLinkProductRepository
   {
       IEnumerable<ModifierLinkProduct> GetModifierLinkProducts(string ProductCode, int ModifierId);
       ModifierLinkProduct GetModifierLinkProductById(string ProductCode, int ModifierId);
       void AddModifierLinkProducts(ModifierLinkProduct tep);
       void UpdateModifierLinkProducts(string id, int ModifierId, ModifierLinkProduct tep);
       void DeleteModifierLinkProducts(int id);
   }
}

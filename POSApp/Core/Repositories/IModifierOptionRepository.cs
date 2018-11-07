using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POSApp.Core.Models;

namespace POSApp.Core.Repositories
{
    public interface IModifierOptionRepository
    {
        IEnumerable<ModifierOption> GetModifierOptions(int storeId);
        ModifierOption GetModifierOptionsById(int id, int storeId);
        void AddModifierOption(ModifierOption mo);
        void UpdateModifierOptions(int id, int storeId, ModifierOption mo);
        void DeleteModifierOptions(int id, int storeId);
        void DeleteModifierOptionsByModifierId(int id, int storeId);
    }
}

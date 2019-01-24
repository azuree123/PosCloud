using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using POSApp.Core.Models;

namespace POSApp.Core.Repositories
{
    public interface IModifierRepository
    {
        IEnumerable<Modifier> GetModifiers(int storeId);
        Modifier GetModifierById(int id, int storeId);
        void AddModifier(Modifier modifier);
        void UpdateModifier(int id, int storeId, Modifier modifier);
        void DeleteModifier(int id, int storeId);
        Task<IEnumerable<Modifier>> GetModifiersAsync(int storeId);
        Task<Modifier> GetModifierByIdAsync(int id, int storeId);
        Task AddModifierAsync(Modifier modifier);
    }
}
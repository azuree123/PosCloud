using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using POSApp.Core.Models;
using POSApp.Core.ViewModels;

namespace POSApp.Core.Shared
{
    public class Helper
    {
        public static List<ModifierOptionViewModel> TempModifierOptions;
        public static void AddToTempModifierOptions(ModifierOptionViewModel modifierOptionViewModel, string userId)
        {
            
            ModifierOptionViewModel checkTrans = TempModifierOptions
                .Where(a => a.Name == modifierOptionViewModel.Name && a.Price.Equals(modifierOptionViewModel.Price)  && a.CreatedBy == userId).ToList()
                .FirstOrDefault();
            modifierOptionViewModel.CreatedBy = userId;
            if (checkTrans != null)
            {
                TempModifierOptions.Remove(checkTrans);
                TempModifierOptions.Add(modifierOptionViewModel);
            }
            else
            {
                ModifierOptionViewModel transDetail = modifierOptionViewModel;
                TempModifierOptions.Add(transDetail);
            }

        }
        public static void RemoveFromTempModifierOptions(string product, int storeId, string userId)
        {
            ModifierOptionViewModel transDetail = TempModifierOptions
                .Where(a => a.Name == product && a.CreatedBy == userId && a.StoreId == storeId).ToList().FirstOrDefault();
            TempModifierOptions.Remove(transDetail);
        }
        public static void EmptyTempModifierOptions(string userId, int storeId)
        {
            List<ModifierOptionViewModel> transDetail = TempModifierOptions
                .Where(a => a.CreatedBy == userId && a.StoreId == storeId).ToList().ToList();
            foreach (var transDetailViewModel in transDetail)
            {
                TempModifierOptions.Remove(transDetailViewModel);
            }
        }
    }
}
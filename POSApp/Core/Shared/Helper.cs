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
        public static List<ProductSubViewModel> TempComboOptions;
        public static void AddToTempComboOptions(ProductSubViewModel ComboOptionViewModel, string userId)
        {

            ProductSubViewModel checkTrans = TempComboOptions
                .Where(a => a.ProductCode == ComboOptionViewModel.ProductCode && a.CreatedBy == userId).ToList()
                .FirstOrDefault();
            ComboOptionViewModel.CreatedBy = userId;
            if (checkTrans != null)
            {
                TempComboOptions.Remove(checkTrans);
                TempComboOptions.Add(ComboOptionViewModel);
            }
            else
            {
                ProductSubViewModel transDetail = ComboOptionViewModel;
                TempComboOptions.Add(transDetail);
            }

        }
        public static void RemoveFromTempComboOptions(string product, int storeId, string userId)
        {
            ProductSubViewModel transDetail = TempComboOptions
                .Where(a => a.ProductCode == product && a.CreatedBy == userId && a.StoreId == storeId).ToList().FirstOrDefault();
            TempComboOptions.Remove(transDetail);
        }
        public static void EmptyTempComboOptions(string userId, int storeId)
        {
            List<ProductSubViewModel> transDetail = TempComboOptions
                .Where(a => a.CreatedBy == userId && a.StoreId == storeId).ToList().ToList();
            foreach (var transDetailViewModel in transDetail)
            {
                TempComboOptions.Remove(transDetailViewModel);
            }
        }







        public static List<ModifierOptionViewModel> TempModifierOptions;
        public static void AddToTempModifierOptions(ModifierOptionViewModel ModifierOptionViewModel, string userId)
        {

            ModifierOptionViewModel checkTrans = TempModifierOptions
                .Where(a => a.Name == ModifierOptionViewModel.Name && a.CreatedBy == userId).ToList()
                .FirstOrDefault();
            ModifierOptionViewModel.CreatedBy = userId;
            if (checkTrans != null)
            {
                TempModifierOptions.Remove(checkTrans);
                TempModifierOptions.Add(ModifierOptionViewModel);
            }
            else
            {
                ModifierOptionViewModel transDetail = ModifierOptionViewModel;
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
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Web;

namespace POSApp.Core.Models
{
    public class Recipe : AuditableEntity
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }


        public string ProductCode { get; set; }
        public Product Product { get; set; }


        public string IngredientCode { get; set; }
        public Product Ingredient { get; set; }

        public decimal Quantity { get; set; }


        public int UnitId { get; set; }
        public  Unit Unit { get; set; }


        public decimal? Calories { get; set; }


        


    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace POSApp.Core.Models
{
    public class Recipe : AuditableEntity
    {
        
        public int Id { get; set; }
        public int StoreId { get; set; }
        public virtual Store Store { get; set; }


        public string ProductCode { get; set; }
        public virtual Product Product { get; set; }

        public DateTime ExpiryDate { get; set; }

        public string IngredientCode { get; set; }
        public virtual Product Ingredient { get; set; }
        public decimal Quantity { get; set; }


        


        public decimal? Calories { get; set; }


        


    }
}
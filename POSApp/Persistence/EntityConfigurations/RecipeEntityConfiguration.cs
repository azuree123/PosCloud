using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using POSApp.Core.Models;

namespace POSApp.Persistence.EntityConfigurations
{
    public class RecipeEntityConfiguration : EntityTypeConfiguration<Recipe>
    {
        public RecipeEntityConfiguration()
        {
            ToTable("Recipes", PosDbContext.DEFAULT_SCHEMA);
            //******************************************************************************************* KEYS ********************
            HasKey(x => new { x.Id,x.IngredientCode, x.StoreId, x.ProductCode });
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //******************************************************************************************* PROPERTIES ***************
            Property(x => x.Quantity).HasColumnType("decimal").IsRequired();
            Property(x => x.ExpiryDate).HasColumnType("datetime").IsRequired();
            Property(x => x.Calories).HasColumnType("decimal").IsOptional();
            Property(x => x.Code).HasColumnType("varchar").HasMaxLength(150).IsOptional();

            //******************************************************************************************* Auditable ***************



            //******************************************************************************************* Auditable ***************

            HasRequired(x => x.Store).WithMany(x => x.Recipes).HasForeignKey(x => x.StoreId).WillCascadeOnDelete(false);
            HasRequired(x => x.Product).WithMany(x => x.Recipes).HasForeignKey(x => new { x.ProductCode, x.StoreId }).WillCascadeOnDelete(false);
            HasRequired(x => x.Ingredient).WithMany(x => x.IngredientRecipes).HasForeignKey(x => new { x.IngredientCode, x.StoreId }).WillCascadeOnDelete(false);
            



        }
    }
}
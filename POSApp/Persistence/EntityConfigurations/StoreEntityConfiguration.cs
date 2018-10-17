﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using POSApp.Core.Models;

namespace POSApp.Persistence.EntityConfigurations
{
    public class StoreEntityConfiguration:EntityTypeConfiguration<Store>
    {
        public StoreEntityConfiguration()
        {
            ToTable("Stores", PosDbContext.DEFAULT_SCHEMA);

            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.Name).HasColumnType("varchar").HasMaxLength(150).IsRequired();
            Property(x => x.Address).HasColumnType("varchar").HasMaxLength(150).IsOptional();
            Property(x => x.Contact).HasColumnType("varchar").HasMaxLength(150).IsOptional();
            Property(x => x.State).HasColumnType("varchar").HasMaxLength(150).IsOptional();
            Property(x => x.City).HasColumnType("varchar").HasMaxLength(150).IsOptional();
            Property(x => x.IsOperational).HasColumnType("bit").IsOptional();
        }
    }
}
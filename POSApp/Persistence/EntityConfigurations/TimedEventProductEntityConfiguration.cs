﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using POSApp.Core.Models;

namespace POSApp.Persistence.EntityConfigurations
{
    public class TimedEventProductEntityConfiguration : EntityTypeConfiguration<TimedEventProducts>
    {

        public TimedEventProductEntityConfiguration()
        {
            ToTable("TimedEventProducts", PosDbContext.DEFAULT_SCHEMA);
            //******************************************************************************************* KEYS ********************
            HasKey(x => new{x.ProductId,x.StoreId,x.TimedEventId});
            
            //******************************************************************************************* PROPERTIES ***************
           

            //******************************************************************************************* Auditable ***************

            //******************************************************************************************* Auditable ***************

            HasRequired(x => x.Store).WithMany(x => x.TimedEventProducts).HasForeignKey(x => x.StoreId).WillCascadeOnDelete(true);
            HasRequired(x => x.TimedEvent).WithMany(x => x.TimedEventProducts).HasForeignKey(x => new{x.StoreId,x.TimedEventId}).WillCascadeOnDelete(true);
            HasRequired(x => x.Product).WithMany(x => x.TimedEventProducts).HasForeignKey(x => new { x.StoreId, x.ProductId }).WillCascadeOnDelete(true);

        }

    }
}
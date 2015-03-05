﻿using System.Data.Entity.ModelConfiguration;
using XPence.Models;

namespace XPence.DbAccess.Implementations.Configuration
{
    public class ComponentMap : EntityTypeConfiguration<Component>
    {
        public ComponentMap()
        {
            //Key
            HasKey(c => c.Id);

            //table
            ToTable("Component");

            //relationship between Component and Layer
            HasRequired(c=>c.Layer).WithMany(l=>l.Components).HasForeignKey(c=>c.LayerId).WillCascadeOnDelete(false);

            //relationship between Component and ComponentFunction
            HasRequired(c => c.ComponentFunction).WithMany(l => l.Components).HasForeignKey(c => c.IsStorageOwner).WillCascadeOnDelete(false);
        }
    }
}

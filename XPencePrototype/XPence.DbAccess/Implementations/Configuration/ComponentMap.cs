using System.Data.Entity.ModelConfiguration;
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

            //relationship
            HasRequired(c=>c.Layer).WithMany(l=>l.Components).HasForeignKey(c=>c.LayerId).WillCascadeOnDelete(false);
        }
    }
}

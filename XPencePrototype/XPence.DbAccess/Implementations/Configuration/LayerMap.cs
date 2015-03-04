using System.Data.Entity.ModelConfiguration;
using XPence.Models;

namespace XPence.DbAccess.Implementations.Configuration
{
    public class LayerMap : EntityTypeConfiguration<Layer>
    {
        public LayerMap()
        {
            //key  
            HasKey(t => t.Id);

            //table  
            ToTable("Layer");  

        }
    }
}

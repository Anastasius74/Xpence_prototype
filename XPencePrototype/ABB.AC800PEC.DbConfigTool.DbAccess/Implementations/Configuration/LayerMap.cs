using System.Data.Entity.ModelConfiguration;
using ABB.AC800PEC.DbConfigTool.Models;

namespace ABB.AC800PEC.DbConfigTool.DbAccess.Implementations.Configuration
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

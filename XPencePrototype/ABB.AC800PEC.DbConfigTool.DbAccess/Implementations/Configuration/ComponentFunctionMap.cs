using System.Data.Entity.ModelConfiguration;
using ABB.AC800PEC.DbConfigTool.Models;

namespace ABB.AC800PEC.DbConfigTool.DbAccess.Implementations.Configuration
{
    public class ComponentFunctionMap : EntityTypeConfiguration<ComponentFunction>
    {
        public ComponentFunctionMap()
        {
            //key  
            HasKey(t => t.Id);

            //table  
            ToTable("component_function");

        }
    }
}

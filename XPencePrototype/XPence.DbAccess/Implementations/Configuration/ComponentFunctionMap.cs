using System.Data.Entity.ModelConfiguration;
using XPence.Models;

namespace XPence.DbAccess.Implementations.Configuration
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

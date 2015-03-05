using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ABB.AC800PEC.DbConfigTool.Models
{
    [Table("component_dependency")]
    public class ComponentDependency
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Required]
        [Column("component")]
        public long Component { get; set; }

        [Required]
        [Column("dependency")]
        public long Dependency { get; set; }
        
        public static ComponentDependency Createcomponent_dependency(long  id, long  component, long  dependency)
        {
            var componentDependency = new ComponentDependency {Id = id, Component = component, Dependency = dependency};

            return componentDependency;
        }
    }
}
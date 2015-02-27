using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XPence.Models
{
    [Table("component_function")]
    public class ComponentFunction
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Required]
        [Column("component")]
        public long Component { get; set; }

        [Required]
        [Column("name")]
        public string Name { get; set; }

        [Column("is_storage_owner")]
        public long? IsStorageOwner { get; set; }

        public static ComponentFunction CreatecomponentFunction(long id, long component, string name)
        {
            var componentFunction = new ComponentFunction {Id = id, Component = component, Name = name};

            return componentFunction;
        }
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XPence.Models
{
    [Table("component_state")]
    public class ComponentState
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("parameter")]
        public long? Parameter { get; set; }

        public static ComponentState Createcomponent_state(long id)
        {
            var componentState = new ComponentState {Id = id};

            return componentState;
        }
    }
}
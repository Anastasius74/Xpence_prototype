using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XPence.Models
{
    [Table("component_role")]
    public class ComponentRole
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Required]
        [Column("component")]
        public long Component { get; set; }

        [Required]
        [Column("role")]
        public long Role { get; set; }
    }
}

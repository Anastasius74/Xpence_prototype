using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ABB.AC800PEC.DbConfigTool.Models
{
    [Table("instance_role")]
    public class InstanceRole
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("function_instance")]
        public long? FunctionInstance { get; set; }

        [Required]
        [Column("role")]
        public long Role { get; set; }

        public static InstanceRole Createinstance_role(long id, long role)
        {
            var instanceRole = new InstanceRole {Id = id, Role = role};

            return instanceRole;
        }
    }
}
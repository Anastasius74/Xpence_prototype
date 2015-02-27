using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XPence.Models
{
    [Table("node_role")]
    public class NodeRole
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Required]
        [Column("node")]
        public long Node { get; set; }

        [Required]
        [Column("role")]
        public long Role { get; set; }

        [Required]
        [Column("group")]
        public long GroupItem { get; set; }

        public static NodeRole Createnode_role(long id, long node, long role, long groupItem)
        {
            var nodeRole = new NodeRole {Id = id, Node = node, Role = role, GroupItem = groupItem};
            return nodeRole;
        }
    }
}
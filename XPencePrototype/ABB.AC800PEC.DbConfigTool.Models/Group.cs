using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ABB.AC800PEC.DbConfigTool.Models
{
    [Table("group")]
    public class Group
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        public static Group Create(long id)
        {
            var group = new Group();
            group.Id = id;
            return group;
        }
    }
}
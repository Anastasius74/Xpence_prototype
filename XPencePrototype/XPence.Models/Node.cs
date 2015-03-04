using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XPence.Models
{
    [Table("node")]
    public sealed class Node
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Required]
        [Column("network_name")]
        public string NetworkName { get; set; }

        //public ICollection<Role> Roles { get; set; }

        public static Node Create(long id, string networkName)
        {
            var node = new Node {Id = id, NetworkName = networkName};

            return node;
        }
    }
}
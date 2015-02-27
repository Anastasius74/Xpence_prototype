using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XPence.Models
{
    [Table("component")]
    public sealed class Component
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Required]
        [Column("name")]
        public string Name { get; set; }

        [Required]
        [Column("is_storage_owner")]
        public long IsStorageOwner { get; set; }

        [Required]
        [Column("layer")]
        public long Layer { get; set; }
       
        [Column("core")]
        public long? Core { get; set; }
    
        public ICollection<Role> Roles { get; set; }
    }
}
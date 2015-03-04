using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XPence.Models
{
    [Table("component")]
    public class Component
    {
        [Column("id")]
        public long Id { get; set; }

        [Required]
        [Column("name")]
        public string Name { get; set; }

        [Required]
        [Column("is_storage_owner")]
        public long IsStorageOwner { get; set; }
       
        [Column("core")]
        public long? Core { get; set; }

        [Column("module")]
        public string Module { get; set; }
       
        [Column("layer")]
        public long LayerId { get; set; }

        [ForeignKey("Id")]
        public virtual Layer Layer { get; set; }

        //public ICollection<Role> Roles { get; set; }
    }
}
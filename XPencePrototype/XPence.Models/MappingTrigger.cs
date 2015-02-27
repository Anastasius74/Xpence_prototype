using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XPence.Models
{
    [Table("mapping_trigger")]
    public class MappingTrigger
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("list")]
        public long? List { get; set; }

        [Column("function")]
        public long? Function { get; set; }

        public static MappingTrigger Createmapping_trigger(long id)
        {
            var mappingTrigger = new MappingTrigger {Id = id};

            return mappingTrigger;
        }
    }
}
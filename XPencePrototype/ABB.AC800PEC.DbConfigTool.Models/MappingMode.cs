using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ABB.AC800PEC.DbConfigTool.Models
{
    [Table("mapping_mode")]
    public class MappingMode
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Required]
        [Column("label")]
        public string Label { get; set; }

        public static MappingMode Createmapping_mode(long id, string label)
        {
            var mappingMode = new MappingMode {Id = id, Label = label};

            return mappingMode;
        }
    }
}
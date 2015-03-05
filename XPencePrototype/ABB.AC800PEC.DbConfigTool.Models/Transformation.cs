using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ABB.AC800PEC.DbConfigTool.Models
{
    [Table("transformation")]
    public class Transformation
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("scaling_offset")]
        public double? ScalingOffset { get; set; }

        [Column("scaling_factor")]
        public double? ScalingFactor { get; set; }

        public static Transformation Create(long id)
        {
            var transformation = new Transformation {Id = id};

            return transformation;
        }
    }
}
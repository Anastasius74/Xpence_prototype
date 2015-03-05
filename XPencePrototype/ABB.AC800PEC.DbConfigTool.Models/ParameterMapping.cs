using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ABB.AC800PEC.DbConfigTool.Models
{
    [Table("parameter_mapping")]
    public class ParameterMapping
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Required]
        [Column("mapping")]
        public long Mapping { get; set; }

        [Required]
        [Column("source")]
        public long Source { get; set; }

        [Required]
        [Column("destination")]
        public long Destination { get; set; }

        [Column("transformation")]
        public long? Transformation { get; set; }

        public static ParameterMapping Createparameter_mapping(long id, long mapping, long source, long destination)
        {
            var parameterMapping = new ParameterMapping
            {
                Id = id,
                Mapping = mapping,
                Source = source,
                Destination = destination
            };

            return parameterMapping;
        }
    }
}
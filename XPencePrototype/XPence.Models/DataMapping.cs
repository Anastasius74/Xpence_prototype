using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XPence.Models
{
    [Table("data_mapping")]
    public class DataMapping
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Required]
        [Column("source")]
        public long Source { get; set; }

        [Required]
        [Column("destination")]
        public long Destination { get; set; }

        [Column("sync_factor")]
        public long? SyncFactor { get; set; }

        [Required]
        [Column("mapping_mode")]
        public long MappingMode { get; set; }

        [Column("key")]
        public long? Key { get; set; }

        [Column("output_trigger")]
        public long? OutputTrigger { get; set; }

        public static DataMapping Createdata_mapping(long id, long source, long destination, long mappingMode)
        {
            var dataMapping = new DataMapping
            {
                Id = id,
                Source = source,
                Destination = destination,
                MappingMode = mappingMode
            };

            return dataMapping;
        }
    }
}
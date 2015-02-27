using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XPence.Models
{
    [Table("parameter")]
    public class Parameter
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Required]
        [Column("component")]
        public long Component { get; set; }

        [Required]
        [Column("name")]
        public string Name { get; set; }

        [Required]
        [Column("data_type")]
        public long DataType { get; set; }

        [Column("inital_value")]
        public string InitialValue { get; set; }

        [Column("min_value")]
        public string MinValue { get; set; }

        [Column("max_value")]
        public string MaxValue { get; set; }

        [Column("address")]
        public string Address { get; set; }

        public static Parameter Create(long id, long component, string name, long dataType)
        {
            var parameter = new Parameter {Id = id, Component = component, Name = name, DataType = dataType};

            return parameter;
        }
    }
}
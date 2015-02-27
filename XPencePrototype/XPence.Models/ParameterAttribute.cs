using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XPence.Models
{
    [Table("parameter_attribute")]
    public class ParameterAttribute
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Required]
        [Column("name")]
        public string Name { get; set; }

        [Required]
        [Column("parameter")]
        public long Parameter { get; set; }

        [Column("value")]
        public string Value { get; set; }

        [Column("data_type")]
        public long? DataType { get; set; }

        public static ParameterAttribute Createparameter_attribute(long id, string name, long parameter)
        {
            var parameterAttribute = new ParameterAttribute {Id = id, Name = name, Parameter = parameter};

            return parameterAttribute;
        }
    }
}
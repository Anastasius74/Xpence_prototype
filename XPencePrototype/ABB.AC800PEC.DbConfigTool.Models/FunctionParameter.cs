using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ABB.AC800PEC.DbConfigTool.Models
{
    [Table("function_parameter")]
    public class FunctionParameter
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("component_function")]
        public long? ComponentFunction { get; set; }

        [Required]
        [Column("parameter")]
        public long Parameter { get; set; }

        [Column("direction")]
        public long Direction { get; set; }

        public static FunctionParameter Createfunction_parameter(long id, long parameter, long direction)
        {
            var functionParameter = new FunctionParameter {Id = id, Parameter = parameter, Direction = direction};

            return functionParameter;
        }
    }
}
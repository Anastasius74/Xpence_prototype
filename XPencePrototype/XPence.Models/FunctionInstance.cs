using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XPence.Models
{
    [Table("function_instance")]
    public class FunctionInstance
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Required]
        [Column("component_function")]
        public long ComponentFunction { get; set; }

        [Required]
        [Column("execution_mode")]
        public long ExecutionMode { get; set; }

        [Required]
        [Column("period_ns")]
        public long PeriodNs { get; set; }

        [Column("is_stateless")]
        public long? IsStateless { get; set; }

        [Column("core")]
        public long? Core { get; set; }

        [Column("key")]
        public long? Key { get; set; }

        public static FunctionInstance Createfunction_instance(long id, long componentFunction, long executionMode,
            long periodNs)
        {
            var functionInstance = new FunctionInstance
            {
                Id = id,
                ComponentFunction = componentFunction,
                ExecutionMode = executionMode,
                PeriodNs = periodNs
            };

            return functionInstance;
        }
    }
}
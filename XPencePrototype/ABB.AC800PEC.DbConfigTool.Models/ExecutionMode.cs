using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ABB.AC800PEC.DbConfigTool.Models
{
    [Table("execution_mode")]
    public class ExecutionMode
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Required]
        [Column("label")]
        public string Label { get; set; }

        public static ExecutionMode Createexecution_mode(long id, string label)
        {
            var executionMode = new ExecutionMode {Id = id, Label = label};

            return executionMode;
        }
    }
}
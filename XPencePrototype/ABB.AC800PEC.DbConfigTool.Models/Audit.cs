using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ABB.AC800PEC.DbConfigTool.Models
{
    [Table("AuditLog")]
    public class Audit
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public DateTime AuditDateInUtc { get; set; }

        [Required]
        public AuditState AuditState { get; set; }

        [Required]
        [MaxLength(100)]
        public string TableName { get; set; }

        [Required]
        [MaxLength(100)]
        public string RecordId { get; set; }

        [MaxLength(100)]
        public string ColumnName { get; set; }

        public string OriginalValue { get; set; }

       public string NewValue { get; set; }
    }

    public enum AuditState
    {
        Added,
        Modified,
        Deleted
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XPence.Models
{
    [Table("direction")]
    public class Direction
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("label")]
        public string Label { get; set; }

        public static Direction Create(long id)
        {
            var direction = new Direction {Id = id};

            return direction;
        }
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XPence.Models
{
    [Table("layer")]
    public class Layer
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Required]
        [Column("label")]
        public string Label { get; set; }

        public static Layer Create(long id, string label)
        {
            var layer = new Layer {Id = id, Label = label};

            return layer;
        }
    }
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ABB.AC800PEC.DbConfigTool.Models
{
    [Table("layer")]
    public class Layer
    {
        [Column("id")]
        public long Id { get; set; }
      
        [Column("label")]
        public string Label { get; set; }

        public virtual ICollection<Component> Components { get; set; }

        //public static Layer Create(long id, string label)
        //{
        //    var layer = new Layer {Id = id, Label = label};

        //    return layer;
        //}
    }
}
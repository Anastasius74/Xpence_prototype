using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XPence.Models
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
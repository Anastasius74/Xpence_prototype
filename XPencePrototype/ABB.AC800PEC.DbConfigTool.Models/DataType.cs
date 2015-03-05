using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ABB.AC800PEC.DbConfigTool.Models
{
    [Table("data_type")]
    public class DataType
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("label")]
        public string Label { get; set; }

        public static DataType Createdata_type(long id, string label)
        {
            var dataType = new DataType {Id = id, Label = label};

            return dataType;
        }
    }
}